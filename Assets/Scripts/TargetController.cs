using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {
    public GameObject target;
    public Vector3 spawnPoint;

    public void RandomTarget()
    {
        // Generate random spawn values
        Vector3 targetPosition = new Vector3(
            Random.Range(-spawnPoint.x, spawnPoint.x),
            Random.Range(3, spawnPoint.y),
            Random.Range(-spawnPoint.z, spawnPoint.z)
        );
        // Instantiate a new target at those values
        Instantiate(target, targetPosition, Quaternion.identity);
    }
}
