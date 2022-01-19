using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RouletteController : MonoBehaviour
{
    [HideInInspector] public GameObject roulette;
    [HideInInspector] public float rotatePerRoulette;
    [HideInInspector] public RouletteMaker rMaker;
    private string result;
    public string gyou;
    private float rouletteSpeed;
    private float slowDownSpeed;
    private int frameCount;
    private bool isPlaying;
    private bool isStop;
    [SerializeField] private Text resultText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button[] hiraganaButtons;
    public bool isTall;//大文字かどうか

    public void SetRoulette () {
        isPlaying = false;
        isStop = false;
        startButton.gameObject.SetActive (true);
        stopButton.gameObject.SetActive (false);
        retryButton.gameObject.SetActive(false);
        startButton.onClick.AddListener (StartOnClick);
        stopButton.onClick.AddListener (StopOnClick);
        retryButton.onClick.AddListener (RetryOnClick);
    }

    private void Update () {
        if (!isPlaying) return;
        roulette.transform.Rotate (0, 0, rouletteSpeed);
        frameCount++;
        if (isStop && frameCount > 3) {
            rouletteSpeed *= slowDownSpeed;
            slowDownSpeed -= 0.25f * Time.deltaTime;
            frameCount = 0;
        }
        if (rouletteSpeed < 0.05f) {
            isPlaying = false;
            ShowResult (roulette.transform.eulerAngles.z);
        }
    }

    private void StartOnClick () {
        rouletteSpeed = 14f;
        startButton.gameObject.SetActive (false);
        Invoke ("ShowStopButton", 1.5f);
        isPlaying = true;
    }

    private void StopOnClick () {
        slowDownSpeed = Random.Range (0.92f, 0.98f);
        isStop = true;
        stopButton.gameObject.SetActive (false);
    }

    private void RetryOnClick(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStopButton () {
        stopButton.gameObject.SetActive (true);
    }

    private void ShowResult (float x) {
        for (int i = 1; i <= rMaker.choices.Count; i++) {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette)))) {
                result = rMaker.choices[i - 1];
            }
        }
        if(result == "k"||result =="K"){
            gyou = "か";
        }else if(result == "s"||result =="S"){
            gyou = "さ";
        }else if(result == "t"||result =="T"){
            gyou = "た";
        }else if(result == "n"||result =="N"){
            gyou = "な";
        }else if(result == "h"||result =="H"){
            gyou = "は";
        }else if(result == "m"||result =="M"){
            gyou = "ま";
        }else if(result == "y"||result =="Y"){
            gyou = "や";
        }else if(result == "r"||result =="R"){
            gyou = "ら";
        }else if(result == "w"||result =="W"){
            gyou = "わ";}

        resultText.text = result+"  ("+gyou+"行)" + "\nが選ばれた！";
        retryButton.gameObject.SetActive(true);
        hiraganaButtons[0].GetComponentInChildren<Text> ().text ="o";

    }
}
