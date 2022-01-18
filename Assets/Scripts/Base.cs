using UnityEngine;

public class Base : MonoBehaviour
{
    public HealthBar healthBar;
    public float baseMaxHealth = 300;
    public float baseHealth;

    private AudioSource audioData;



    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(baseMaxHealth);

        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void DamageReceived(int damage)
    {

        healthBar.slider.value -= damage;
        audioData.Play(0);
    }

    public void HealthRestored(int health)
    {
        healthBar.slider.value += health;
    }

    public void UpgradeHealth(int health)
    {
        healthBar.slider.maxValue += health;
        healthBar.slider.value = healthBar.slider.maxValue;
    }
}
