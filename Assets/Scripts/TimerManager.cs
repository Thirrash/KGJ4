using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    public Text ResultText;

    private Text TimerText;
    private float timeLeft = 185.0f;
    private bool bIsFinished = false;

    public float minScore = 4.76f;

    void Start()
    {
        ResultText.text = "";
        TimerText = GetComponent<Text>();
        StartCoroutine(UpdateText());
        StartCoroutine(UpdateObjective());
    }

    private void Update()
    {
        if (bIsFinished) { return; }

        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            StopCoroutine(UpdateText());
            if (CostManager.Instance.TotalCost > minScore)
            {
                Win();
            }
            else
            {
                Lose();
            }

            bIsFinished = true;
        }
    }

    private IEnumerator UpdateText()
    {
        while (true)
        {
            int time = (int)timeLeft;
            int minutes = time / 60;
            int seconds = time - minutes * 60;

            TimerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator UpdateObjective()
    {
        ResultText.text = "Destroy Lootz City\nScore at least 4,76 PLN";
        yield return new WaitForSeconds(5.0f);
        ResultText.text = "";

    }

    private void Win() { ResultText.text = "!You win!\n\nPress Escape to return to Menu"; }

    private void Lose() { ResultText.text = "!You lose!\n\nPress Escape to return to Menu"; }
}
