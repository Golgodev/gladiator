using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PushEnemy") {
            other.gameObject.GetComponent<Animator>().SetTrigger("Push");
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
