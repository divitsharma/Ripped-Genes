using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    List<BasePair> basePairs = new List<BasePair>();

    public float TimerDuration = 100;
    float timeRemaining;
    float second = 1f;

    public TMP_Text timerText;

    void Start()
    {
        timeRemaining = TimerDuration;
    }

    public void AddBasePair(BasePair bp)
    {
        basePairs.Add(bp);
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            second -= Time.deltaTime;
        }

        if (second <= 0)
        {
            timerText.text = ((int)timeRemaining).ToString();
            //Debug.Log((int)timeRemaining);
            second = 1f;
        }

        if (timeRemaining <= 0)
        {
            StartCoroutine("GameOver");
        }
    }

    IEnumerator GameOver()
    {
        int nBps = basePairs.Count;
        int nBroken = 0;
        foreach (BasePair basePair in basePairs)
        {
            if (basePair.IsBroken())
            {
                nBroken++;
            }
        }

        if (nBroken > nBps / 2)
        {
            Debug.Log("The virus won!");
        }
        else
        {
            Debug.Log("The virus lost!");
        }

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(0);
    }

}
