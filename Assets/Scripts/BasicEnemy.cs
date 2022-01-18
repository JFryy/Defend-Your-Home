using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class BasicEnemy : MonoBehaviour
{
    public int shotsToKill = 5;
    private int shotsCounted = 0;
    private bool isDying = false;
    private AudioSource audioData;
    public int scorePerKill = 20;
    private bool fallBufferEnabled = false;
    public float elapsedTime = 0.0f;
    public float secondsBetweenAttack = 1;
    public int AtkDamage = 2;
    private GameObject mainBase;
    public float staggerPeriod = 0.6f;
    public float deathHeight;
    public float speed;
    private bool isBeingHeld = false;
    public float attackRange = 3.1f;
    public Animator Animator;
    public bool isGrounded;
    public GameObject bloodExplosion;

    public GameObject pointAnimationPrefab;
    private bool deathHeightReached = false;
    private bool scoreCounted = false;

    void Start()
    {
        mainBase = GameObject.Find("Base");
        audioData = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        Vector3 position = this.gameObject.transform.localPosition;
        AttackTurret();
        if (position.y > deathHeight && !isBeingHeld)
        {
            deathHeightReached = true;
        }


        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, this.gameObject.transform.position.z);
        }

        // do nothing if death heigh reached
        else if (deathHeightReached || isDying) { }

        else if (fallBufferEnabled)
        {
            Animator.SetBool("IsHurt", true);
            Animator.SetBool("IsAttacking", false);
            Animator.SetBool("IsMoving", false);
            Animator.SetBool("IsDying", false);
            staggerPeriod -= Time.deltaTime;
            if (staggerPeriod <= 0)
            {
                fallBufferEnabled = false;
                staggerPeriod = 0.6f;
            }

        }

        else if (AttackTurret()){}
        else if (InAttackRange())
        {
            if (elapsedTime > secondsBetweenAttack)
            {
                elapsedTime = 0;
                Attack();
                Animator.SetBool("IsHurt", false);
                Animator.SetBool("IsAttacking", true);
                Animator.SetBool("IsMoving", false);
                Animator.SetBool("IsDying", false);
            }
        }
        else if (isGrounded && !deathHeightReached)
        {
            Animator.SetBool("IsHurt", false);
            Animator.SetBool("IsAttacking", false);
            Animator.SetBool("IsMoving", true);
            Animator.SetBool("IsDying", false);
            if (mainBase.transform.position.x - transform.position.x > 0)
            {
                this.gameObject.transform.localPosition = new Vector3(position.x + speed, position.y, position.z);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                this.gameObject.transform.localPosition = new Vector3(position.x - speed, position.y, position.z);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // ensure we're colliding with the actual floor.

        if (collision.gameObject.layer == 6 //check the int value in layer manager(User Defined starts at 8) 
             && !isGrounded)
        {
            isGrounded = true;
        }

        if (deathHeightReached && collision.gameObject.layer == 6)
        {
            string collisionTag = collision.gameObject.tag;
            if (collisionTag == "Enemy" || collisionTag == "Archer" || collisionTag == "TankEnemy"){
                scorePerKill += scorePerKill;
            }
            Animator.SetBool("IsHurt", false);
            Animator.SetBool("IsAttacking", false);
            Animator.SetBool("IsMoving", false);
            Animator.SetBool("IsDying", true);
            Destroy(gameObject, 0.6f);
            audioData.Play(0);
            InstantiateScoreCounter();
            InstantiateBloodExplosion();
        }
        else
        {
            fallBufferEnabled = true;
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            isBeingHeld = true;
            Animator.SetBool("IsHurt", true);
            Animator.SetBool("IsAttacking", false);
            Animator.SetBool("IsMoving", false);
            Animator.SetBool("IsDying", false);
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = mousePos - transform.position;
        dir.Normalize();
        Rigidbody2D body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = dir * 8f;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6
            && isGrounded)
        {
            isGrounded = false;
        }
    }

    bool InAttackRange()
    {
        if (System.Math.Abs(mainBase.transform.position.x - transform.position.x) < attackRange)
        {
            return true;
        }
        return false;
    }

    bool AttackTurret()
    {
        GameObject[] turretObjs;
        turretObjs = GameObject.FindGameObjectsWithTag("Turret");
        foreach(GameObject turret in turretObjs) 
        {
            if ((System.Math.Abs(turret.transform.position.x - transform.position.x) < 2f && System.Math.Abs(turret.transform.position.y - transform.position.y) < 1f)) 
            {
                if (elapsedTime > secondsBetweenAttack) 
                {
                    elapsedTime = 0;
                    turret.GetComponent<Turret>().DealDamage();
                    Animator.SetBool("IsHurt", false);
                    Animator.SetBool("IsAttacking", true);
                    Animator.SetBool("IsMoving", false);
                    Animator.SetBool("IsDying", false);
                    return true;
                }
                return true;
            }
        }
        return false;
    }
    void Attack()
    {
        mainBase.GetComponent<Base>().DamageReceived(AtkDamage);
    }

    public void Kill()
    {
        Animator.SetBool("IsHurt", false);
        Animator.SetBool("IsAttacking", false);
        Animator.SetBool("IsMoving", false);
        Animator.SetBool("IsDying", true);
        Destroy(gameObject, 0.6f);
        isDying = true;
        InstantiateScoreCounter();
        InstantiateBloodExplosion();
    }

    public void GotShot()
    {
        shotsCounted += 1;
        fallBufferEnabled = true;
        audioData.pitch = 1f;
        audioData.Play(0);
        if (shotsCounted >= shotsToKill)
        {
            Kill();
        }
    }

    private void InstantiateScoreCounter() {
        if (!scoreCounted) {
            GameObject go = Instantiate(pointAnimationPrefab, new Vector3(transform.position.x, transform.position.y + .7f, transform.position.z), Quaternion.identity);
            go.transform.SetParent(GameObject.Find("UI").transform, true);
            go.GetComponent<ScrollingText>().FloatPoints(scorePerKill.ToString());
            GameManager.Instance.score += scorePerKill;
            scoreCounted = true;
        }
    }

    private void InstantiateBloodExplosion() {
        GameObject go = Instantiate(bloodExplosion, new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z), Quaternion.identity);
    }

}
