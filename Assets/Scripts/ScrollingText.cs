using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public float durationSeconds = 2;
    // Start is called before the first frame update
    private float seconds = 0f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        seconds += Time.deltaTime;
        if (seconds > durationSeconds){
            Destroy(gameObject);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y+.01f, transform.position.z);
        
    }

    public void FloatPoints(string points){
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "+" + points;
    }
}
