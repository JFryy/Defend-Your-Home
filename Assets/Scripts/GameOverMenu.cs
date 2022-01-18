
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
