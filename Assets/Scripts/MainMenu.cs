using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene("Main");

    }

    public void QuitGame()
    {
        Debug.Log("Game has been exited.");
        Application.Quit();
    }
}
