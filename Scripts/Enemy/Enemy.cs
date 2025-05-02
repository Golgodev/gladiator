using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHealth = 75;
    [SerializeField] AudioSource enemyHitAudio;
    [Space(20)]
    [SerializeField] GameObject enemyDrop;

    public void HitEnemy(int amount) {
        enemyHealth -= amount;

        if(enemyHitAudio != null) 
            enemyHitAudio.Play();

        if(enemyHealth <= 0) {
            Die();
        }
    }

    bool ChechHeatlh() {
        if(enemyHealth < enemyHealth / 2.5)
            return true;
        else
            return false;
    }

    public void Die() {
        if(enemyDrop != null) {
            enemyDrop.transform.position = gameObject.transform.position;
            enemyDrop.transform.SetParent(null);
            enemyHitAudio.Play();
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
