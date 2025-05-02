using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform[] spawnPoints;
    
    public Event cutScene;

    public void StartWave() {
        for(int i = 0; i < spawnPoints.Length; i++) {
            Instantiate(enemyToSpawn, spawnPoints[i]);
        }
        Destroy(gameObject);
    }
}
