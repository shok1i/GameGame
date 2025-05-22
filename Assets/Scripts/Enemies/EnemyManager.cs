using System.Collections;
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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

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
            else if (!isAttacking && !isDeath)
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
    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
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
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        isAttacking = false;
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
            isDeath = true;
            StartCoroutine(DeathCoroutine());
            
        }
    }
}
