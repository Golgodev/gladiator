using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UpgradeSystem: MonoBehaviour
{
    public static int curPoints = 1;
    public TMP_Text showcasePoints;
    public TMP_Text pointText;
    public TMP_Text healthText;
    public TMP_Text staminaText;
    public TMP_Text damageText;
    public TMP_Text speedText;
    static PlayerStats ps;
    static PlayerMovement pm;
    static Weapon weapon;
    public static bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Main-Sword").GetComponent<Weapon>();
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();   
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();   
        UpdateText();
    }

    public void AddPoint() {
        curPoints++;
        showcasePoints.text = curPoints.ToString();
    }

    public static void ChangeState() {
        ps.enabled = true;
        pm.enabled = true;
        weapon.enabled = true;
        isActive = false;   
    }

    public void AddHealth() {
        if(curPoints > 0) {
            PlayerStats.maxPlayerHealth += 10;
            curPoints--;
            UpdateText();
        }
    }

    public void AddStamina() {
        if(curPoints > 0) {
            PlayerStats.maxPlayerStamina += 10;
            curPoints--;
            UpdateText();
        }
    }

    public void AddDamage() {   
        if(curPoints > 0) {
            Weapon.weaponDamage += 5;
            curPoints--;
            UpdateText();
        }
    }

    public void AddSpeed() {
        if(curPoints > 0) {
            PlayerMovement.speed += 0.3f;
            pm.defaultSpeed += 0.3f;
            curPoints--;
            UpdateText();
        }
    }

    public void UpdateText() {
        healthText.text = PlayerStats.maxPlayerHealth.ToString();
        staminaText.text = PlayerStats.maxPlayerStamina.ToString();
        speedText.text = PlayerMovement.speed.ToString();
        damageText.text = Weapon.weaponDamage.ToString();

        pointText.text = curPoints.ToString();
    }
}
