using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemy : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootingPoint;
    public float shotCooldown = 3.5f;
    private float lastShot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShot + shotCooldown)
        {
            Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.rotation);
            lastShot = Time.time;
        }
    }
}
