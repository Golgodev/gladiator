using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static int weaponDamage = 35;
    [SerializeField] AudioSource swingAudio;
    [SerializeField] float weaponCooldown = 0.8f;
    [SerializeField] int staminaCost = 20;
    [SerializeField] float canHitDuration = 0.35f;
    Animator weaponAnimator;
    public GameObject bloodParticlePrefab;
    [SerializeField] Collider2D coll;
    float lastAttack;
    Block block;
    PlayerStats ps;

    // Start is called before the first frame update
    void Start()
    {
        block = GameObject.Find("Shield").GetComponent<Block>();
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();   
        weaponAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > lastAttack + weaponCooldown && Time.deltaTime != 0) {
            bool hasActiveBlock = block.IsBlocking();

            if(!hasActiveBlock && ps.playerStamina >= staminaCost) {
                Attack();
            }
        }
    }

    public void Attack() {
            StartCoroutine(SetCollider(canHitDuration));
            swingAudio.Play(0);
            ps.TakeStamina(staminaCost);
            weaponAnimator.SetTrigger("Attack");
            lastAttack = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy") {
           other.gameObject.GetComponent<Enemy>().HitEnemy(weaponDamage);
           Instantiate(bloodParticlePrefab, other.transform.position, other.transform.rotation);
        }

        if(other.gameObject.tag == "Chest") {
           other.gameObject.GetComponent<Chest>().HitChest(weaponDamage);
        }
    }

    public IEnumerator SetCollider(float waitTime) {
        coll.enabled = true;

        yield return new WaitForSeconds(waitTime);
        coll.enabled = false;
    }
}
