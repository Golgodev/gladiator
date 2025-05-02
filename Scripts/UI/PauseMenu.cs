    using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    RotateTowardsMouse rtm;
    PlayerMovement pm;
    private bool isOpen;
    public GameObject pauseMenuObject;
    public bool optionIsActive = false;
    public bool lockpickingActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rtm = GameObject.Find("Player").GetComponent<RotateTowardsMouse>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && optionIsActive != true && lockpickingActive == false) {
            SwitchMenuState();
        }
    }

    public void ChangeBool() {
        if(optionIsActive == true)
            optionIsActive = false;
        else 
            optionIsActive = true;
    }

    public void BackToMenu() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwitchMenuState() {
        if(isOpen) {
            pauseMenuObject.SetActive(false);
            pm.enabled = true;
            rtm.enabled = true;
            Time.timeScale = 1.0f;

            isOpen = false;
        } else {
            pauseMenuObject.SetActive(true);
            pm.enabled = false;
            rtm.enabled = false;
            Time.timeScale = 0f;

            isOpen = true;
        }
    }
}
