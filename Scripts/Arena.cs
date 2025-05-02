using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Arena : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Wave[] waveList;
    public TMP_Text waveText;
    public UnityEvent onWavesCompleted; 
    private int currentWave = 0;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool isWaveActive = false;
    public void TriggerWave()
    {
        isWaveActive = true;
        for (int i = 0; i < waveList[currentWave].enemies.Length; i++)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            GameObject enemy = Instantiate(waveList[currentWave].enemies[i], spawnPoints[randomSpawn].position, spawnPoints[randomSpawn].rotation);
            spawnedEnemies.Add(enemy);
        }

        waveText.text = "Wave " + (currentWave + 1).ToString();
        currentWave++;
        StartCoroutine(CheckWaveStatus());
    }

    private IEnumerator CheckWaveStatus()
    {
        while (isWaveActive)
        {
            spawnedEnemies.RemoveAll(enemy => enemy == null);

            if (spawnedEnemies.Count == 0)
            {
                isWaveActive = false;
                yield return new WaitForSeconds(5f);

                if (currentWave < waveList.Length)
                {
                    TriggerWave();
                }
                else
                {
                    onWavesCompleted.Invoke(); 
                }
            }

            yield return new WaitForSeconds(1f); 
        }
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;
}
