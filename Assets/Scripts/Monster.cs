using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterStatus
{
    Idle,
    Walk,
    Attack,
    Hit,
    Die
}

public class Monster : MonoBehaviour
{
    public PlayerBehavior playerBehavior;
    public Transform playerTransform;   // 플레이어의 위치 정보 참조
    public Transform target;            // 목표 위치
    
    public int maxHp = 100;
    private int currentHp;
    public float moveSpeed = 3.0f;
    private float attackRange = 5.0f;  // 이 범위안에 들어오면 공격
    [SerializeField] float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public int attackDamage = 1;

    private MonsterStatus currentState = MonsterStatus.Idle;
    private bool isAttacking = false;

    private Animator anim;
    public NavMeshAgent navAgent;
    public Transform treasureTransform;

    private bool targetInRange = false;
    private float treasureGuardRange = 30.0f; // Treasure로 돌아갈 범위
    private float treasureAttackRange = 5.0f; // Treasure 공격 범위
    bool isDie = false;
    void Awake()  // Plz use Awake() instead of Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHp = maxHp;
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerBehavior = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();  // prefab disconnected inspector scripts
        treasureTransform = GameObject.FindWithTag("Treasure").transform;
    }

    private void Update()
    {
        if (isDie) return;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        float distanceToTreasure = Vector3.Distance(transform.position, treasureTransform.position);

        // 플레이어가 공격 범위 안에 있는지 확인
        if (distanceToPlayer <= attackRange || distanceToTreasure <= treasureAttackRange)
        {
            targetInRange = true;
        }
        else if (distanceToPlayer > treasureGuardRange || distanceToTreasure < attackRange)
        {
            targetInRange = false;
        }

        // 플레이어 또는 Treasure의 근접성에 따라 목표 결정
        if (targetInRange && currentState != MonsterStatus.Die && currentState != MonsterStatus.Hit)
        {
            if (distanceToPlayer <= attackRange)
            {
                target = playerTransform;
            }
            else if (distanceToTreasure <= treasureAttackRange)
            {
                target = treasureTransform;
            }
        }
        else
        {
            target = treasureTransform;
        }

        // 목표물 (플레이어 또는 treasure)을 향해 이동
        navAgent.SetDestination(target.position);

        // 이동 애니메이션
        if (navAgent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        // 공격 로직
        if (targetInRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (isDie) return;
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

    private void OnTriggerEnter(Collider col){
        if(currentState == MonsterStatus.Die)   return;
        if(col.CompareTag("Weapon")){
            currentHp -= playerBehavior.attackDamage;
            Debug.Log(currentHp);
            currentState = MonsterStatus.Hit;
            if(currentHp <= 0 && !isDie){// prevent times Die
                MonsterDie(); 
                return;
            
            }  
            anim.SetTrigger("GetHit");
        }
    }

    private void MonsterDie(){
        navAgent.isStopped = true;
        navAgent.enabled = false;
        anim.SetTrigger("Die");
        isDie = true;
        GameManager.Instance.UpdateRemainingMonster();
        // 코인 생성
        Instantiate(GameManager.Instance.coin, transform.position + new Vector3(0,3f,0), Quaternion.identity);

        Destroy(gameObject,4f);
    }
}