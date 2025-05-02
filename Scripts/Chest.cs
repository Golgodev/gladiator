using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int chestHealth = 45;
    [HideInInspector] public Animator chestAnimator;

    public void HitChest(int amount) {
        chestAnimator.SetTrigger("Hit");
        chestHealth -= amount;

        if(chestHealth <= 0) {
            DestroyChest();
        }
    } 

    public void DestroyChest() {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        chestAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
