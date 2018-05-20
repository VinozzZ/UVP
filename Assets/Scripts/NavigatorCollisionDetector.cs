using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using UnityEngine;

public class NavigatorCollisionDetector : MonoBehaviour {
    private SteamVR_Controller.Device navigator;
    private GameObject explorer;
    private Component gameController;
    private float distanceBetween;
    private bool navigatorIsInTarget = false;

	private void Start()
	{
        navigator = GameObject.Find("GameCanvas").GetComponent<GameController>().navigatorController;
        explorer = GameObject.Find("Explorer Controller");
	}
	private void OnTriggerStay(Collider other)
	{
        
        // explorer.transform.position

        gameController = GameObject.Find("GameCanvas").GetComponent<GameController>();
        if(other.gameObject.tag == "target")
        {
            navigatorIsInTarget = true;
            distanceBetween = Vector3.Distance(other.transform.position, explorer.transform.position);
        }
	}

    private void Update()
    {
        if(navigatorIsInTarget) 
        { 
            
            CalcVibration();
            navigatorIsInTarget = false;
        }
    } 
    private void CalcVibration()
    {
        if (distanceBetween < 1)
        { 
            distanceBetween = 1;
        }
        ushort vibrationIntensity =  (ushort) (10000 / (int) distanceBetween);
        navigator.TriggerHapticPulse(vibrationIntensity);
    }
}
