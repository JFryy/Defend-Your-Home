using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float shotsPerSecond = 1;
    public float timeElapsed = 0;

    public int health = 7;
    public float turretRange = 10;

    public bool isBeingHeld = true;
    public float turretOffsetY = .2f;

    public bool isFiring = false;

    private AudioSource audioData;

    void Start()
    {
        transform.GetComponent<Animator>().SetBool("IsFiring", false);
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isBeingHeld)
        {
            Vector3 position = this.gameObject.transform.localPosition;
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.localPosition = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            return;
        }

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= shotsPerSecond)
        {
            Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(transform.localPosition.x - turretRange, transform.localPosition.y - turretOffsetY));
            List<Collider2D> enemyList = new List<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                // sort this by closest enemy.
                if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Archer" || collider.gameObject.tag == "TankEnemy")
                {
                    enemyList.Add(collider);
                }

            }
            if (enemyList.Count != 0)
            {
                Collider2D nearestEnemy = null;
                float closestDistance = 1000000f;
                foreach (Collider2D collider in enemyList)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestEnemy = collider;
                    }
                }
                if (nearestEnemy.gameObject.tag == "Archer")
                {
                    nearestEnemy.gameObject.GetComponent<GoblinArcher>().GotShot();

                }
                else if (nearestEnemy.gameObject.tag == "Enemy")
                {
                    nearestEnemy.gameObject.GetComponent<BasicEnemy>().GotShot();

                }
                else if (nearestEnemy.gameObject.tag == "TankEnemy")
                {
                    nearestEnemy.gameObject.GetComponent<MushroomEnemy>().GotShot();

                }
                transform.GetComponent<Animator>().SetBool("IsFiring", true);
                timeElapsed = 0;
            }
            else
            {
                transform.GetComponent<Animator>().SetBool("IsFiring", false);
            }
        }
        SpriteRenderer turretSR = gameObject.GetComponent<SpriteRenderer>();
        turretSR.color = new Color(255,255,255,255);
    }

    void OnMouseUp()
    {
        isBeingHeld = false;
    }

    public void DealDamage() {
        health -= 1;
        audioData.Play(0);
        // add some death animation
        SpriteRenderer turretSR = gameObject.GetComponent<SpriteRenderer>();
        turretSR.color = new Color(255,0,0,255);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
