using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class Cutscenes : MonoBehaviour
{
    public float speed = 5f;
    public GameObject[] cutscenesHolder;
    public UnityEvent[] endEvents;
    Transform target; 
    Camera mainCamera;
    GameObject playerObject;
    int cameraMoveIndex = 0;
    int cutsceneIndex = 0;
    bool isPlaying;
    GameObject hudObject;

    static Animator topBar;
    static Animator botBar;
    static bool isRolled = false;

    // Start is called before the first frame update
    void Awake()
    {
        topBar = GameObject.Find("TopBar").GetComponent<Animator>();
        botBar = GameObject.Find("BotBar").GetComponent<Animator>();

        playerObject = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();    
        hudObject = GameObject.Find("HUD");

        target = cutscenesHolder[0].transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && isPlaying == true)
        {
            playerObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            playerObject.GetComponent<PlayerMovement>().enabled = false;
            Vector3 direction = target.position - transform.position;
            float distanceToTarget = direction.magnitude;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);

            if (distanceToTarget < 0.1f)
            {
                if(cameraMoveIndex >= cutscenesHolder[cutsceneIndex].transform.childCount - 1) {
                    EndCutscene();
                    return;
                }

                transform.position = target.position;
                cameraMoveIndex++;
                if(cameraMoveIndex < cutscenesHolder[cutsceneIndex].transform.childCount) {
                    target = cutscenesHolder[cutsceneIndex].gameObject.transform.GetChild(cameraMoveIndex).transform;
                } else {
                    EndCutscene();
                }
            }
        }
    }

    public void EndCutscene() {
        isPlaying = false;
        UseBars();
        playerObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        playerObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        playerObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = true;
        hudObject.SetActive(true);
        endEvents[cutsceneIndex].Invoke();
        cutsceneIndex++;
        cameraMoveIndex = 0;
        target = null;
    }

    public void TriggerCutscene() {
        UseBars();
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = false;
        hudObject.SetActive(false);
        target = cutscenesHolder[cutsceneIndex].transform.GetChild(0);
        isPlaying = true;
    }

    
    public static void UseBars() {
        if(!isRolled) {
            topBar.SetBool("Slided", true);
            botBar.SetBool("Slided", true);
            isRolled = true;
        } else {
            topBar.SetBool("Slided", false);
            botBar.SetBool("Slided", false);
            isRolled = false;
        }
    }
}
