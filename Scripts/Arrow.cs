 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow: MonoBehaviour
{
    public Rigidbody2D rb;
    public float arrowSpeed = 3f;
    PlayerStats ps;

    // Start is called before the first frame update
    void Awake()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();   
        rb.AddForce(transform.right * arrowSpeed);
    }



    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            ps.TakeDamage(35);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
