using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    public Transform target;       // 목표 위치
    public NavMeshAgent navAgent;   
    public Animator anim;

    
    void Awake()  // Plz use Awake() instead of Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(target.position);
    }
}
