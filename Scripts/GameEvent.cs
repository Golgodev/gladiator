using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    public UnityEvent gameStart;
    public float startCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGameEvent());       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGameEvent() {
        yield return new WaitForSeconds(startCooldown);    
        gameStart.Invoke();
    }
}
