using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject Canvas;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && Time.timeScale != 0)
        {
            PauseGame();
            isPaused = true;


        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
            isPaused = false;
        }

    }

    void PauseGame()
    {
        Time.timeScale = 0;
        Canvas.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        Canvas.gameObject.SetActive(false);
    }


}
