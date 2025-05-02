using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int enemyDamage = 300;
    [SerializeField] float enemyAttackCooldown = 3.5f;
    public float attackRange = 2f; 
    public bool isBlocking;
    public Animator anim;
    private float lastUsedTime; 
    public Block block;
    public float notifyBeforeAttack = 0.5f;
    public GameObject player;
    PlayerStats ps;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerStats>();   
    }

    private void Start() {
        if(block == null) {
            block = GameObject.Find("Shield").GetComponent<Block>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= attackRange && Time.time > lastUsedTime + enemyAttackCooldown)
        {
            StartCoroutine("PrepareAttack");
        }
    }

    public void Attack() {
        bool hasActiveBlock = block.IsBlocking();

        if(!hasActiveBlock && Vector2.Distance(transform.position, player.transform.position) <= attackRange) {
            ps.TakeDamage(enemyDamage);
        } else if(hasActiveBlock) {
            block.blockAudio.Play();
        }

    }

    private IEnumerator PrepareAttack()
    {
        anim.SetBool("PrepAttack", true);
        lastUsedTime = Time.time;
        yield return new WaitForSeconds(notifyBeforeAttack);
        anim.SetBool("PrepAttack", false);
        anim.SetTrigger("Attack");
        Attack();
    }
}
