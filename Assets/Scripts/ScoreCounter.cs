using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{



    public TextMeshProUGUI MyText;
    public int score;


    // Use this for initialization
    void Start()
    {

        score = 0;

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        MyText.text = "Score:" + score.ToString();

    }


    public void AddPoints(int Points)
    {

        score += Points;

    }
}