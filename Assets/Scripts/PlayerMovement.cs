using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float step;
    public float tState = 0;

    private float offset = 0;

    public bool isEnemy;

    HelixCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        if (isEnemy) {
            offset = Mathf.PI;
        }
        curve = GameObject.FindObjectOfType<HelixCurve>();
        Vector3 curvePos = curve.Ft(tState, offset);

        this.transform.position = curvePos;        
        Debug.Log(this.transform.position);
        Debug.Log(curvePos);

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (isEnemy) {
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
                Debug.Log("right clicked");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                this.updatePosition("left");
                Debug.Log("left clicked");
            }
        } else {
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
                Debug.Log("right clicked");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.updatePosition("left");
                Debug.Log("left clicked");
            }
        }
    }

    private void updatePosition(string direction) {
        Transform playerPos = this.transform;
        Vector3 curvePos = curve.Ft(tState, offset);
        Vector3 curveInc = new Vector3();
        switch(direction) {
            case "up" :
                // around helix body top
            case "down" :
                // around helix body bottom
            case "left" :
                // a = helixCurve(t.pos + step)
                // Calculate direction to a
                // transform pos by a.norm * speed
                tState -= step;
                curveInc = curve.Ft(tState - step, offset);                
                break;
            case "right" :
                tState += step;
                curveInc = curve.Ft(tState + step, offset);
                break;
            
        }
            this.transform.position = Vector3.Slerp(playerPos.position, curveInc, Speed * Time.deltaTime);
            this.transform.rotation = Quaternion.FromToRotation(playerPos.up, curveInc.normalized);
    }
}
