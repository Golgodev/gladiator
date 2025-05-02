using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LockedDoor : MonoBehaviour
{
    PlayerMovement pm;
    RotateTowardsMouse rtm;
    public GameObject lockedGameObject;
    bool isActive;
    public Slider gameSlider;
    public Slider showcaseSlider;
    public int needLock = 10;
    public float speed = 10;
    int curLock = 0;
    public TMP_Text lockProgressionText;
    public UnityEvent events;

    Task task;
    AudioSource hitLock;
    AudioSource missLock;
    AudioSource succsessLock;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        rtm = GameObject.Find(("Player")).GetComponent<RotateTowardsMouse>();
        task = GameObject.Find(("GameManager")).GetComponent<Task>();

        hitLock = GameObject.Find("Hit").GetComponent<AudioSource>();
        missLock = GameObject.Find("Miss").GetComponent<AudioSource>();
        succsessLock = GameObject.Find("Succsess").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && Time.timeScale != 0) {
            gameSlider.value += 1f / 5f * Time.deltaTime * (60 * speed);

            if(Input.GetKeyDown(KeyCode.Space) && gameSlider.value > showcaseSlider.value - 10 && gameSlider.value < showcaseSlider.value + 10) {
                curLock++;
                hitLock.Play();
                SetSliders();

            } else if(Input.GetKeyDown(KeyCode.Space) && gameSlider.value < showcaseSlider.value - 10 || gameSlider.value > showcaseSlider.value + 13 && Input.GetKeyDown(KeyCode.Space) || gameSlider.value >= 100) {
                curLock = 0;
                missLock.Play();
                SetSliders();
            }
        }
    }

    public void SetSliders() {
        gameSlider.value = 0;
        lockProgressionText.text = curLock + "/" + needLock;
        int randomNumber = Random.Range(40, 85);
        showcaseSlider.value = randomNumber;

        if(curLock == needLock) {
            Success();
        }
    }

    public void Success() {
        pm.enabled = true;
        rtm.enabled = true;
        lockedGameObject.SetActive(false);   
        isActive = false;
        GameObject.Find("Canvas").GetComponent<PauseMenu>().lockpickingActive = false;
        succsessLock.Play();

        events.Invoke();

        Destroy(gameObject);
    }

    public void TriggerMinigame() {
        SetSliders();   
        pm.enabled = false;
        rtm.enabled = false;
        GameObject.Find("Canvas").GetComponent<PauseMenu>().lockpickingActive = false;
        lockedGameObject.SetActive(true);   
        isActive = true;
    }
}
