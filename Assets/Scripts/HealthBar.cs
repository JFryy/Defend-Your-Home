using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.value = slider.maxValue;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Update()
    {
        if (slider.value <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        // Make a game over scene or animation
        SceneManager.LoadScene("GameOverScreen");
    }

}
