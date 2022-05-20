using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public int numberGladiator;
    public bool isFriend;
    public bool isAlive;

    Animator anim;

    public float speed;
    private Transform player;
    private Transform targetGladiator;

    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

    public float attackRange = 1f;

    public Transform PositionPoint;
    public Transform PositionPoint2;
    public Transform RightPointAttack;
    public Transform UpPointAttack;
    public Transform LeftPointAttack;
    public Transform DownPointAttack;
    public float attackRadius = 0.5f;
    public LayerMask friendLayer;
    public float attackDamage = 2f;

    public float maxHealth = 5f;
    float currentHealth;
    public float fill;
    public Image bar;
    public Sprite friendBar;
    public Sprite enemyBar;
    public Canvas hpBar;

    // Start is called before the first frame update
    void Start()
    {
        targetGladiator = PositionPoint2;

        currentHealth = maxHealth;
        fill = 1f;
        if (isFriend)
        {
            bar.sprite = friendBar;
        } else
        {
            bar.sprite = enemyBar;
        }

        anim = GetComponentInChildren<Animator>();

        if (!isAlive)
        {
            TakeDamage(maxHealth);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        fill = (currentHealth / maxHealth);
        bar.fillAmount = fill;

        Collider2D[] moveTarget = Physics2D.OverlapCircleAll(PositionPoint.position, 100000f, friendLayer);
        
        foreach (Collider2D enemy in moveTarget)
        {
            if (enemy.GetComponent<EnemyController>().enabled)
            {

                if (enemy.GetComponent<EnemyController>().getNumberGladiator() != numberGladiator)
                {
                    Transform enemyPosition = enemy.GetComponent<Transform>();
                    if (enemy.GetComponent<EnemyController>().isAlive && targetGladiator.position.sqrMagnitude > enemyPosition.position.sqrMagnitude)
                    {
                        targetGladiator = enemyPosition;
                    }
                }
            }  else
            {
                if (!isFriend && enemy.GetComponent<GladiatorController>().enabled)
                {
                    Transform enemyPosition = enemy.GetComponent<Transform>();
                    if (targetGladiator.position.sqrMagnitude > enemyPosition.position.sqrMagnitude)
                    {
                        targetGladiator = enemyPosition;
                    }
                }
            }
        }

    
        transform.position = Vector2.MoveTowards(transform.position, targetGladiator.position, speed * Time.deltaTime);

        Vector2 direction = targetGladiator.position - transform.position;

        if (Vector2.Distance(targetGladiator.position, transform.position) < attackRange && Time.time >= nextAttackTime)
        {
            anim.SetBool("Attack", true);

            if (anim.GetBool("WalkRight"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(RightPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<GladiatorController>().enabled)
                    { 
                        enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        if (enemy.GetComponent<EnemyController>().getNumberGladiator() != numberGladiator)
                        {
                            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                        }
                    }
                }
            }

            if (anim.GetBool("WalkUp"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(UpPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<GladiatorController>().enabled)
                    {
                        enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        if (enemy.GetComponent<EnemyController>().getNumberGladiator() != numberGladiator)
                        {
                            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                        }
                    }
                }
            }

            if (anim.GetBool("WalkLeft"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(LeftPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<GladiatorController>().enabled)
                    {
                        enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        if (enemy.GetComponent<EnemyController>().getNumberGladiator() != numberGladiator)
                        {
                            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                        }
                    }
                }
            }

            if (anim.GetBool("WalkDown"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(DownPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<GladiatorController>().enabled)
                    {
                        enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<EnemyController>().enabled)
                    {
                        if (enemy.GetComponent<EnemyController>().getNumberGladiator() != numberGladiator)
                        {
                            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                        }
                    }
                }
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (Vector2.Angle(Vector2.right, direction) <= 45f || Vector2.Angle(Vector2.right, direction) >= 315f)
        {
            anim.SetBool("WalkRight", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("WalkRight", false);

        }

        if (Vector2.Angle(Vector2.down, direction) <= 225f && Vector2.Angle(Vector2.down, direction) >= 135f)
        {
            anim.SetBool("WalkUp", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("WalkUp", false);

        }

        if (Vector2.Angle(Vector2.right, direction) <= 225f && Vector2.Angle(Vector2.right, direction) >= 135f)
        {
            anim.SetBool("WalkLeft", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("WalkLeft", false);

        }

        if (Vector2.Angle(Vector2.up, direction) <= 225f && Vector2.Angle(Vector2.up, direction) >= 135f)
        {
            anim.SetBool("WalkDown", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("WalkDown", false);

        }
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
            isAlive = false;
            hpBar.enabled = false;
            anim.SetBool("Die", true);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }

    public int getNumberGladiator()
    {
        return numberGladiator;
    }

}
