using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    GameObject playerObject;

    Slider healthSlider;
    Slider staminaSlider;

    public static float maxPlayerStamina = 100;
    public float playerStamina;  

    public static float maxPlayerHealth = 100f; 
    float playerHealth; 
    public GameObject deadMenu;

    public float staminaRegenRate = 35f;
    public float staminaRegenDelay = 0.5f;
    public AudioClip player_hit_1;
    public AudioClip player_hit_2;
    public AudioSource ac;

    private Coroutine regenCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        staminaSlider = GameObject.Find("StaminaSlider").GetComponent<Slider>();
        playerObject = GameObject.Find("Player");

        deadMenu = GameObject.Find("DeadMenu");
        deadMenu.SetActive(false);

        playerHealth = maxPlayerHealth;
        playerStamina = maxPlayerStamina;
        SetSlider();
    }

    public void TakeDamage(float amount)
    {
        playerHealth -= amount;
        SetSlider();

        int chooseSound = Random.Range(0,2);
        if(chooseSound == 0) {
            ac.clip = player_hit_1;
            ac.Play();
        } else {
            ac.clip = player_hit_2;        
            ac.Play();
        }

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void TakeStamina(int amount)
    {
        if (playerStamina < amount)
            return;

        playerStamina -= amount;
        SetSlider();

        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
        }
        regenCoroutine = StartCoroutine(RegenerateStamina());
    }

    void Die()
    {
        Debug.Log("you died");
        deadMenu.SetActive(true);

        Destroy(playerObject); 
    }

    void SetSlider()
    {
        healthSlider.maxValue = maxPlayerHealth;
        healthSlider.value = playerHealth;

        staminaSlider.maxValue = maxPlayerStamina;
        staminaSlider.value = playerStamina;
    }

    IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(staminaRegenDelay);

        while (playerStamina < maxPlayerStamina)
        {
            playerStamina += staminaRegenRate * Time.deltaTime;
            SetSlider();
            yield return null;
        }

        playerStamina = maxPlayerStamina;
        SetSlider();
        regenCoroutine = null;
    }
}
