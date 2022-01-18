using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float countDown = 2f;
    public bool isBeingHeld = true;
    private bool detonationStarted = false;

    void FixedUpdate()
    {

        if (isBeingHeld && !detonationStarted)
        {
            Vector3 position = this.gameObject.transform.localPosition;
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.localPosition = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
        else if (!isBeingHeld)
        {
            countDown -= Time.deltaTime;
            // do some minute time.delta iteration
            if (countDown <= 0)
            {
                Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ensure we're colliding with the actual floor.
        if (collision.gameObject.layer == 6 && !isBeingHeld)
        {
            detonationStarted = true;

        }
    }

    void OnMouseUp()
    {
        isBeingHeld = false;
    }
}
