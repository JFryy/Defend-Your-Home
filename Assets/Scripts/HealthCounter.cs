
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    public TextMeshProUGUI MyText;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MyText.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
}
