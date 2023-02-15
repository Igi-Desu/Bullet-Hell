using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayingTime : MonoBehaviour
{
    int CurrentSeconds => GameManager.Instance.CurrentSeconds;
    TMP_Text time;
    // Start is called before the first frame update
    void Start()
    {
        time=GetComponent<TMP_Text>();
        GameManager.Instance.AddSecondEvent(ChangeSeconds);

    }

    // Update is called once per frame
    void ChangeSeconds()
    {   
        int minutes = CurrentSeconds/60;
        string minutesString = minutes<10? "0"+minutes.ToString(): minutes.ToString();
        int seconds = CurrentSeconds%60;
        string secondsString = seconds<10? "0"+seconds.ToString(): seconds.ToString();
        time.text = minutesString + ":" + secondsString;
    }
}
