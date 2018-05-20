using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerCollisionDetector: MonoBehaviour {
    private SteamVR_Controller.Device explorer;
    private bool explorerIsInTarget = false;
    private int screenFlashTimer;
    private GameController gameController;
    private AudioSource audioSource;
    public AudioClip pointGained;
    public AudioClip misclick;

	private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameCanvas").GetComponent<GameController>();
        explorer = gameController.explorerController;
        screenFlashTimer = -1;
	}

	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "target")
        {
            explorerIsInTarget = true;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag == "target")
        {
            explorerIsInTarget = false;
        }
	}

	void Update()
	{ 
        if (screenFlashTimer == 0)
        {
            gameController.ChangeBackgroundColor(new Color(0,0,1,1));
        }
        if (screenFlashTimer > -1)
        { 
            screenFlashTimer -= 1;
            Debug.Log(screenFlashTimer);
        }

        if ( explorer.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) 
        {
            screenFlashTimer = 90;
            if (explorerIsInTarget)
            {
                gameController.ChangeBackgroundColor(new Color(0,1,0,1));
                audioSource.PlayOneShot(pointGained, 1f);
                gameController.AddScore();
                explorerIsInTarget = false; 
                gameController.ModifyTime(5);
            }
            else
            {
                gameController.ChangeBackgroundColor(new Color(1,0,0,1));
                audioSource.PlayOneShot(misclick, 1f);
                gameController.ModifyTime(-1);
            }
        }
	}
}