using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Animator anim;

    public float speed;
    private Transform player;

    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

    public float attackRange = 1f;

    public Transform RightPointAttack;
    public Transform UpPointAttack;
    public Transform LeftPointAttack;
    public Transform DownPointAttack;
    public float attackRadius = 0.5f;
    public LayerMask friendLayer;
    public int attackDamage = 2;

    public int maxHealth = 5;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        anim = GetComponentInChildren<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        Vector2 direction = player.position - transform.position;

        if (Vector2.Distance(player.position, transform.position) < attackRange && Time.time >= nextAttackTime)
        {
            anim.SetBool("Attack", true);

            if (anim.GetBool("WalkRight"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(RightPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
                }
            }

            if (anim.GetBool("WalkUp"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(UpPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
                }
            }

            if (anim.GetBool("WalkLeft"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(LeftPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
                }
            }

            if (anim.GetBool("WalkDown"))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(DownPointAttack.position, attackRadius, friendLayer);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<GladiatorController>().TakeDamage(attackDamage);
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

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;

        if (currentHealth <= 0)
        {
            anim.SetBool("Die", true);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }


}
