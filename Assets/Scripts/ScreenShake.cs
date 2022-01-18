using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake


    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.06f;

    public float shakeDuration;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 0.5f;

    // The initial position of the GameObject
    Vector3 initialPosition;
    // Start is called before the first frame update

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }

}
