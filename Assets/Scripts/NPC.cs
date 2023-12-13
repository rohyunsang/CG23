using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public GameObject gptManager;
    public string characterConcept = "";
    public NavMeshAgent navAgent;
    
    public Vector3 originalPosition; // 원래 위치를 저장할 변수
    private Animator animator;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position; // 초기 위치 저장
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    public void GoShelter()
    {
        navAgent.SetDestination(GameManager.Instance.shelterPosition.position);
        animator.SetBool("isWalking", true); // 걷기 애니메이션 활성화
    }

    public void GoToOriginalPosition()
    {
        navAgent.SetDestination(originalPosition);
        animator.SetBool("isWalking", true); // 걷기 애니메이션 활성화
    }


    void Update() 
    {
        // 게임이 일시정지 상태일 때만 ESC 키를 감지합니다.
        if (GameManager.Instance.isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.GameContinue();
            GameManager.Instance.OffChatScreen();
        }

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            animator.SetBool("isWalking", false); // 걷기 애니메이션 비활성화
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            gptManager.GetComponent<ChatGPT>().characterConcept = characterConcept;
            GameManager.Instance.OnChatScreen();
            GameManager.Instance.GamePause();
            GameManager.Instance.FirstInvasion();
        }
    }
}