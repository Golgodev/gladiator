using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class StoryTeller : MonoBehaviour
{
    public string[] storyText; 
    private TMP_Text text;
    private bool isActive = false;
    private int indexOfStory = -1;
    public float typingSpeed = 0.025f;
    public UnityEvent events;

    void Start()
    {
        text = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        NextText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isActive)
        {
            NextText();
        }
    }

    public void NextText()
    {
        indexOfStory++;

        if (indexOfStory == storyText.Length)
        {
            isActive = false;
            events.Invoke();
            return;
        }

        StartCoroutine(TypeText(storyText[indexOfStory]));
    }

    private IEnumerator TypeText(string story)
    {
        text.text = "";
        isActive = true;

        foreach (char letter in story.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isActive = false;
    }
}
