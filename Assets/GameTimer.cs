using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    string text;
    TextMeshProUGUI timer;

    private void Awake() {
        timer = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTimer(float time){
        int minutes = ((int)(time/60.0));
        int seconds = ((int)(time%60.0));
        timer.text = string.Format("{0:00}:{1:00}", minutes,seconds);
    }
}
