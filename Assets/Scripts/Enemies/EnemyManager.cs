using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework.Constraints;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health;
    public float damage;
    private Animator _animator;
    public GameObject target;
    public float speed;
    public float distanceToFollow;
    public float distanceToAttack;

    public bool isDistanceFighter;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float distance;

    private bool isAttacking = false;
    private bool isDeath = false;

    void Start()
    {
        target = GameObject.FindWithTag("Player").gameObject;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        if (!isDistanceFighter)
        {
            if (distance < distanceToFollow)
            {
                if (target.transform.position.x < transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                }
                if (distance < distanceToAttack)
                {
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        StartCoroutine(AttackCoroutine());
                    }
                }
                if (!isAttacking && !isDeath)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
                    _animator.SetBool("IsRunning", true);
                }
            }
            else
            {
                _animator.SetBool("IsRunning", false);
            }
        }
        else if (isDistanceFighter)
        {
            if (isAttacking) return;
            if (target.transform.position.x < transform.position.x)
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            else
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);

            if (distance < distanceToAttack)
            {
                Vector2 directionAway = (transform.position - target.transform.position).normalized;
                Vector2 targetPos = (Vector2)transform.position + directionAway * speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                _animator.SetBool("IsRunning", true);
            }
            else if (distance <= distanceToFollow)
            {
                if (distance <= distanceToAttack + 0.1f)
                {
                    _animator.SetBool("IsRunning", false);
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        StartCoroutine(AttackDistanceCoroutine());
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                    _animator.SetBool("IsRunning", true);
                    isAttacking = false;
                }
            }
            else
            {
                _animator.SetBool("IsRunning", false);
                isAttacking = false;
            }
        }

    }
    IEnumerator AttackCoroutine()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        _animator.SetBool("IsAttacking", true);
        _animator.SetBool("IsRunning", false);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        if (distance < distanceToAttack)
        {
            _attack();
        }
        _animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Cooldown object" + gameObject);
        isAttacking = false;
    }

    IEnumerator AttackDistanceCoroutine()
    {
        Debug.Log("Starting distance attack");
        _animator.SetBool("IsAttacking", true);
        _animator.SetBool("IsRunning", false);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        if (distance > distanceToAttack)
        {
            Debug.Log("Called method");
            _distanceAttack();
        }
        _animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Shooted");
        isAttacking = false;
    }

    IEnumerator DeathCoroutine()
    {
        _animator.SetBool("IsAttacking", false);
        _animator.SetBool("IsRunning", false);
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
        while (!state.IsName("Death"))
        {
            yield return null;
            state = _animator.GetCurrentAnimatorStateInfo(0);
        }
        while (state.normalizedTime < 1f)
        {
            yield return null;
            state = _animator.GetCurrentAnimatorStateInfo(0);
        }
        Debug.Log("Deleted object" + gameObject);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        isAttacking = false;
    }


    private void _distanceAttack()
    {
        Vector2 direction = (target.transform.position - gameObject.transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log("Bullet created");
    }
    private void _attack()
    {
        target.GetComponent<PlayerManager>().GetComponent<PlayerHealth>().TakeDamage(damage);
    }
    public void getDamage(float playerDamage)
    {
        health -= playerDamage;
        if (health <= 0)
        {
            _animator.SetBool("IsDeath", true);
            Debug.Log("IsDeath for " + gameObject + " is true");
            isDeath = true;
            StartCoroutine(DeathCoroutine());
            
        }
    }
}
