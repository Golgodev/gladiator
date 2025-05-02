using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public Slider maxFPSSlider;
    public TMP_Text fpsText;
    public TMP_Text volumeText;
    public Toggle vSyncToggle;
    public TMP_Dropdown windowModeDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    private int[] resolutionsWidth = { 1920, 1280, 1024 };
    private int[] resolutionsHeight = { 1080, 720, 576 }; 

    private void Start()
    {
        maxFPSSlider.onValueChanged.AddListener(UpdateMaxFPS);
        vSyncToggle.onValueChanged.AddListener(UpdateVSync);
        windowModeDropdown.onValueChanged.AddListener(UpdateWindowMode);
        resolutionDropdown.onValueChanged.AddListener(UpdateResolution);
        volumeSlider.onValueChanged.AddListener(UpdateVolume);

        resolutionDropdown.ClearOptions();
        List<string> resolutionsOptions = new List<string>();
        for (int i = 0; i < resolutionsWidth.Length; i++)
        {
            resolutionsOptions.Add(resolutionsWidth[i] + "x" + resolutionsHeight[i]);
        }
        resolutionDropdown.AddOptions(resolutionsOptions);

        AudioListener.volume = 100;
    }

    public void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void PlayScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpdateMaxFPS(float value)
    {
        int fps = Mathf.RoundToInt(value);
        Application.targetFrameRate = fps;
    }

    public void UpdateVSync(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
    }

    public void UpdateWindowMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    public void UpdateSliders(int value) {
        volumeText.text = volumeSlider.value.ToString();
        fpsText.text = maxFPSSlider.value.ToString();
    }

    public void UpdateResolution(int index)
    {
        Screen.SetResolution(resolutionsWidth[index], resolutionsHeight[index], Screen.fullScreenMode);
    }

    public void UpdateVolume(float value)
    {
        AudioListener.volume = value;
    }
}
