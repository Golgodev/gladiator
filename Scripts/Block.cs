using UnityEngine;

public class Block : MonoBehaviour
{
    bool isBlocking = false;
    private Animator shieldAnimator;
    PlayerMovement pm;
    public float slowEffect;
    public AudioSource blockAudio;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();   
        shieldAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)) {
            isBlocking = true;
            PlayerMovement.speed = slowEffect;
            shieldAnimator.SetBool("Blocking", true);
        } else {
            PlayerMovement.speed = pm.defaultSpeed;
            isBlocking = false;
            shieldAnimator.SetBool("Blocking", false);
        }
    }

    public bool IsBlocking() {
        if(isBlocking == true)
            return true;
        else
            return false;
    }
}
