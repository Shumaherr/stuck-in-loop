using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
 
public class Timer : MonoBehaviour {
    public TextMeshProUGUI timerLabel;
 
    [SerializeField] public float time;
 
    void Update() {
        time -= Time.deltaTime;
 
        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.

        //update the label value
        timerLabel.text = seconds.ToString("0");
    }
}