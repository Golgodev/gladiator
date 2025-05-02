using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;

public class Task : MonoBehaviour
{
    TMP_Text taskText;

    public String[] taskString;
    int index = 0;

    public void NextTask() {
        if(index != taskString.Length) {
            taskText.text = taskString[index];
            index++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        taskText = GameObject.Find("TaskText").GetComponent<TMP_Text>();
        NextTask();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
