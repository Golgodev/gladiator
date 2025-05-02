using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject interactAlertObject;
    public Task task;
    PlayerStats ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();   
    }

    private void OnTriggerStay2D(Collider2D other) {

        switch(other.tag) {
            case "Bars":
                interactAlertObject.SetActive(true);
                if(Input.GetKey(KeyCode.E)) {
                    LockedDoor lockedDoors = other.gameObject.GetComponent<LockedDoor>();
                    lockedDoors.TriggerMinigame();
                }
                break;
            case "Pickable":
                interactAlertObject.SetActive(true);
                if(Input.GetKey(KeyCode.E)) {
                    interactAlertObject.SetActive(false);
                    other.gameObject.GetComponent<Interactable>().Interact();
                }
                break;
            case "NextScene":
                SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "Trigger":
                other.gameObject.GetComponent<Trigger>().TriggerEvent();
                break;
            case "PushEnemy":
                other.gameObject.GetComponent<Animator>().SetTrigger("Push");
                gameObject.GetComponent<AudioSource>().Play();
                break;
            case "Damage":
                ps.TakeDamage(20);
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Spawner") {
                EnemyTrigger enemyTrigger = other.gameObject.GetComponent<EnemyTrigger>();
                enemyTrigger.StartWave();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject != null)
            interactAlertObject.SetActive(false);
    }
    
}
