using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {
    public SteamVR_Controller.Device explorerController;
    public SteamVR_Controller.Device navigatorController;

    private SteamVR_TrackedObject navigator;
    private AudioSource[] audioSources;
    private AudioSource soundFX;
    private AudioSource bgMusic;
    private int score;
    private bool gameRunning = false;
    private TargetController target;
    private Image backgroundImage; 
    public Text scoreText;
    public Text timerText;
    public Text statusText;
    public AudioClip gameOver;
    public AudioClip marioSong;
    float secondsRemaining = 60;

    // Use this for initialization
    void Start () {
        explorerController = SteamVR_Controller.Input(2);
        navigatorController = SteamVR_Controller.Input(1);
        audioSources = GetComponents<AudioSource>();
        soundFX = audioSources[0];
        bgMusic = audioSources[1]; 

        backgroundImage = GameObject.Find("CanvasBackground").GetComponent<Image>();
        target = GameObject.Find("GameCanvas").GetComponent<TargetController>();
        target.RandomTarget();
        score = 0; 
        scoreText.text = "" + score;
        timerText.text = secondsRemaining.ToString();
        statusText.text = "Click to play UVP";
        backgroundImage.color = new Color(0,1,0,0);
	}

    void Update () {
        if (gameRunning) { 
            UpdateTime();
            bgMusic.volume = 0.5f;
            if (secondsRemaining <= 0)
            { 
                gameRunning = false;
                backgroundImage.color = new Color(0,1,0,0);
                timerText.color = new Color(1,0,0,1);                
                scoreText.color = new Color(1,0,0,1);                
                statusText.color = new Color(1,0,0,1);                
                statusText.text = "Game over! Click to try again";
                soundFX.PlayOneShot(gameOver, 1f);
            }
        }
        else 
        { 
            bgMusic.volume = 1f;
            backgroundImage.color = new Color(0,0,0,0);
            if (navigatorController.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            { 
                backgroundImage.color = new Color(0,0,1,1);
                timerText.color = new Color(0,0,0,1);                
                scoreText.color = new Color(0,0,0,1);                
                statusText.color = new Color(0,0,0,1);                
                secondsRemaining = 60;
                statusText.text = "";
                score = 0;
                UpdateScore();
                gameRunning = true; 
            } 
        }
    }

    public void ChangeBackgroundColor(Color color)
    {
        backgroundImage.color = color;
    }
	public void AddScore () {
        GameObject oldTarget = GameObject.FindWithTag("target");
        Destroy(oldTarget);
        target.RandomTarget();

        score += 1;
        UpdateScore();
	}

    private void UpdateScore () {
        scoreText.text = "" + score;
    }

    private void UpdateTime() {
        secondsRemaining -= Time.deltaTime; 
        if (secondsRemaining < 0) 
        {
            secondsRemaining = 0;
        }
        timerText.text = Math.Round(secondsRemaining, 1).ToString();
    }

    public void ModifyTime(int seconds) {
        secondsRemaining += seconds;
    }
}