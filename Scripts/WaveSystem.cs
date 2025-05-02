using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem: MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform[] spawnPoints;

    public Slider waveSlider;

    public int curWave;

    int maxEnemies = 5;
    public List<GameObject> curEnemies;

    public void StartWave() {
        for(int i = 0; i < maxEnemies; i++) {
            int y = Random.Range(1, spawnPoints.Length);

            GameObject enemy = Instantiate(enemyPrefab[0], spawnPoints[y].position, Quaternion.identity);
            curEnemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            StartWave();
        }
    }
}
