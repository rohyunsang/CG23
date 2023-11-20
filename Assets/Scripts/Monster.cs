using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterStatus
{
    Idle,
    Walk,
    Attack,
    Die
}

public class Monster : MonoBehaviour
{
    public Transform playerTransform;   // 플레이어의 위치 정보 참조
    public Transform target;            // 목표 위치
    
    public int maxHealth = 100;
    private int currentHealth;
    public float moveSpeed = 3.0f;
    private float attackRange = 5.0f;  // 이 범위안에 들어오면 공격
    [SerializeField] float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public int damage = 10;

    private MonsterStatus currentState = MonsterStatus.Idle;
    private bool isAttacking = false;

    private Animator anim;

    public NavMeshAgent navAgent;   

    
    void Awake()  // Plz use Awake() instead of Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    private void Start()
    {
        currentHealth = maxHealth;
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerTransform && !isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        currentState = MonsterStatus.Attack;

       
        float randomValue = Random.value; // 0.0f ~ 1.0f 사이의 랜덤한 값을 반환

        if (randomValue <= 0.9f) 
        {
            anim.SetTrigger("Attack01");
        }
        else 
        {
            anim.SetTrigger("Attack02");
        }


        lastAttackTime = Time.time;
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}