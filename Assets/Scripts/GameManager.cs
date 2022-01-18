using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreHolder;
    public Slider health;
    public int score;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        score = 0;
    }

    //Instead let Scoreholder pull this data from the instantiated object, 
    //do not pull anything from the scene into this class.
    void Update()
    {
        ScoreHolder.text = "Score:" + score.ToString();
    }

    public void AddPoints(int Points)
    {
        score += Points;
    }

    public void SubtractPoints(int Points)
    {
        score -= Points;
    }

}