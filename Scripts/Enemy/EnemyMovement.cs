using UnityEngine;
 
public class EnemyMovement : MonoBehaviour
{
    public float maxMoveSpeed = 3f;
    public float minMoveSpeed = 2f;
    private float moveSpeed = 3f;
    public float distance = 2f;
    private Transform target;

    void Start()
    {
        moveSpeed = Random.Range (minMoveSpeed, maxMoveSpeed); 
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = target.position - transform.position;

        if (player != null && Vector2.Distance(transform.position, player.transform.position) >= distance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void PassiveMove() {
            
    }
}