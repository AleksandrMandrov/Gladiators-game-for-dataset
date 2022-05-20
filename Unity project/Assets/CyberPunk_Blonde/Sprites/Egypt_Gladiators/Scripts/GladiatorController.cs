
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class GladiatorController : MonoBehaviour {

    public int numberGladiator = 0;

    SpriteRenderer Srend;
    Animator anim;

    public float attackRate = 1f;
    float nextAttackTime = 0f;

    public Transform RightPointAttack;
    public Transform UpPointAttack;
    public Transform LeftPointAttack;
    public Transform DownPointAttack;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayers;
    public float attackDamage = 2f;

    public float maxHealth = 5;
    float currentHealth;
    public float fill;
    public Image bar;


    //change these variables if you wish to test different speeds and jump heights
    [SerializeField]
    float moveForce = 0.7f;


    //this variable is used for the screen wrapping
    float screenHalfWidthInWorldUnits;

    bool screenWrap = false;



    void Start()
    {
        currentHealth = maxHealth;
        fill = 1f;

        Srend = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        //these lines are used to calculate screen wrapping
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }
    // Update is called once per frame
    void Update () {

        fill = (currentHealth / maxHealth);
        bar.fillAmount = fill;

        //controller and sprite manipulation
        #region
        //controller and sprite manipulation
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            anim.SetBool("Attack", true);

            if (anim.GetBool("WalkRight"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(RightPointAttack.position, attackRadius, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                    }

                }
            }

            if (anim.GetBool("WalkUp"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(UpPointAttack.position, attackRadius, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                    }
                }
            }

            if (anim.GetBool("WalkLeft"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(LeftPointAttack.position, attackRadius, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                    }
                }
            }

            if (anim.GetBool("WalkDown"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(DownPointAttack.position, attackRadius, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                    }
                }
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("WalkUp", true);

            transform.Translate(Vector2.up * Time.fixedDeltaTime);
            anim.SetBool("Idle", false);
        }else
        {
            anim.SetBool("WalkUp", false);

        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("WalkRight", true);
            transform.Translate(Vector2.right * Time.fixedDeltaTime);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("WalkRight", false);

        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("WalkLeft", true);
            transform.Translate(Vector2.left * Time.fixedDeltaTime);

        }
        else 
        {
            anim.SetBool("WalkLeft", false);

        }




        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("WalkDown", true);

            transform.Translate(Vector2.down * Time.fixedDeltaTime);

        }
        else
        {
            anim.SetBool("WalkDown", false);
        }



       

        if (Input.GetKeyDown(KeyCode.Space))
        {

            anim.SetBool("Idle", false);

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetBool("Die", true);
            anim.SetTrigger("Death");
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
       
        }
        #endregion // //controls and sprite manipulation


        //camera wrap
        #region
        //controls the camera wrap
        if (screenWrap)
        {


            if (transform.position.x < -screenHalfWidthInWorldUnits)
            {
                transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
            }

            if (transform.position.x > screenHalfWidthInWorldUnits)
            {
                transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
            }
        }
        #endregion//camera wrap 
    }

    void OnDrawGizmosSelected()
    {
        if (!RightPointAttack || !UpPointAttack || !LeftPointAttack || !DownPointAttack)
        {
            return;
        }

        Gizmos.DrawWireSphere(RightPointAttack.position, attackRadius);
        Gizmos.DrawWireSphere(UpPointAttack.position, attackRadius);
        Gizmos.DrawWireSphere(LeftPointAttack.position, attackRadius);
        Gizmos.DrawWireSphere(DownPointAttack.position, attackRadius);
    }

    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;

        if (currentHealth <= 0)
        {
            bar.fillAmount = 0;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }

    public int getNumberGladiator()
    {
        return numberGladiator;
    }

}
