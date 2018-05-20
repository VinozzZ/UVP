using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePosition : MonoBehaviour {

    private GameObject capsuleContainer;
    private GameObject explorerController;
	// Use this for initialization
    void Start () {
        explorerController = GameObject.Find("Explorer Controller");
        capsuleContainer = GameObject.Find("Capsule Container");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 capsulePosition = new Vector3(
            explorerController.transform.position.x,
            explorerController.transform.position.y,
            explorerController.transform.position.z
        );

        capsuleContainer.transform.position = capsulePosition;
	}
}
