using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum side {
    A = 0,B = 1
}

public class PlayerMovement : MonoBehaviour
{
    public float step;
    public float tState = 0;

    private float offset = 0;

    public bool isEnemy;

    public float minClamp = 5f;
    public float maxClamp = 10f;


    // Side flipping animation

    public side curSide = side.A;
    public float animationSpeed;
    private bool inAnimation = false;
    private float startTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength;


    HelixCurve curve;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        if (isEnemy) {
            curSide = side.B;
        }
        if (curSide == side.B) {
            offset = Mathf.PI;
        }
        curve = GameObject.FindObjectOfType<HelixCurve>();
        Vector3 curvePos = curve.Ft(tState, offset);

        this.transform.position = curvePos;        
        //Debug.Log(this.transform.position);
        //Debug.Log(curvePos);

    }

    void Update() {
        if (!inAnimation) {
            if (curSide == side.B) {
                offset = Mathf.PI;
            }
            if (curSide == side.A) {
                offset = 0;
            }
            if (isEnemy)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (player.useEnergy()) player.DoAction();
                }
                else
                {
                    player.Speed = player.MaxSpeed;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    this.updatePosition("up");
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    this.updatePosition("down");
                }
                if (Input.GetKey(KeyCode.D))
                {
                    this.updatePosition("right");
                    //Debug.Log("right clicked");
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    this.updatePosition("left");
                    //Debug.Log("left clicked");
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (player.useEnergy()) player.DoAction();
                }
                else
                {
                    player.Speed = player.MaxSpeed;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.updatePosition("up");
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.updatePosition("down");
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.updatePosition("right");
                    //Debug.Log("right clicked");
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.updatePosition("left");
                    //Debug.Log("left clicked");
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (inAnimation) {
            float distCovered = (Time.time - startTime) * animationSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            
            if (transform.position == endPos) {
                inAnimation = false;
            }
        } else {
            
        }
    }

    private void updatePosition(string direction) {
        Vector3 curvePos = curve.Ft(tState, offset);
        Vector3 curveInc = new Vector3();
        switch(direction) {
            case "up" :
                this.flip();
                break;
            case "down" :
                this.flip();
                break;
            case "left" :
                // a = helixCurve(t.pos + step)
                // Calculate direction to a
                // transform pos by a.norm * speed
                tState -= player.Speed * Time.deltaTime;
                break;
            case "right" :
                tState += player.Speed * Time.deltaTime;
                break;
        }
        tState = Mathf.Clamp(tState, minClamp * Mathf.PI, maxClamp * Mathf.PI);
        curveInc = curve.Ft(tState, offset);

        // On the right side
        transform.position = curveInc;
        //this.transform.position = Vector3.Slerp(this.transform.position, curveInc, player.Speed * Time.deltaTime);
        // this.transform.rotation = Quaternion.FromToRotation(this.transform.up, curveInc.normalized);
    }

    private void flip() {
        // flip side
        inAnimation = true;
        startTime = Time.time;
        startPos = this.transform.position;
        if (curSide == side.A) {
            endPos = curve.Ft(tState, Mathf.PI);
            journeyLength = Vector3.Distance(startPos, endPos);
        } else {
            endPos = curve.Ft(tState, 0);
            journeyLength = Vector3.Distance(startPos, endPos);
        }
        this.curSide = curSide == side.A ? side.B : side.A;
    }
}
