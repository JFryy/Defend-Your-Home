using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameObject mainCamera;
    public float explosionDuration = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<ScreenShake>().shakeDuration = 0.2f;
        // on start trigger kill within radius?
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        explosionDuration -= Time.deltaTime;
        if (explosionDuration <= 0)
        {
            print(transform.position);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 3.3f);
            Debug.Log(colliders.Length);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    Debug.Log(collider);
                    collider.gameObject.GetComponent<BasicEnemy>().Kill();
                }
                else if (collider.gameObject.tag == "Archer") { collider.gameObject.GetComponent<GoblinArcher>().Kill(); }
                else if (collider.gameObject.tag == "TankEnemy") { collider.gameObject.GetComponent<MushroomEnemy>().Kill(); }
            }


            Destroy(gameObject);
        }
    }
}
