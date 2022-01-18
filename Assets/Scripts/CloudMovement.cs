using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float CloudSpeed = .001f;
    public float screenEnd = 20.5f;
    public float screenBegin = -19.5f;

    void FixedUpdate()
    {
        if (this.gameObject.transform.position.x > screenEnd)
        {
            this.gameObject.transform.position = new Vector3(
            screenBegin,
            this.gameObject.transform.position.y,
            this.gameObject.transform.position.z
        );
        }
        this.gameObject.transform.position = new Vector3(
            this.gameObject.transform.position.x + CloudSpeed,
            this.gameObject.transform.position.y,
            this.gameObject.transform.position.z
        );
    }
}
