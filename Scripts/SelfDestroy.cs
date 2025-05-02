using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeToDestroy = 3f; 
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }

}
