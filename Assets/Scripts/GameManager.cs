using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject ChatScreen;
    public GameObject monsterSpwanManagerObj;
    public bool firstInvade = true;

    public float timerDuration = 10f; // This is the duration you set for the timer
    public float currentTimer; // This will be shown in the inspector
    public bool isPaused { get; private set; } = false;  // 게임의 일시정지 상태를 나타내는 변수

    public int currentMonster = 0; // current number of field monsters
    public TextMeshProUGUI remainingMonstersText;

    public GameObject[] npcs;

    public TextMeshProUGUI restStageTimerText;
    public Transform shelterPosition;

    public GameObject coin;
    public int coinCount = 0;

    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 이 오브젝트가 씬 전환시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 GameManager 인스턴스가 있다면, 현재 인스턴스를 파괴
        }

        Cursor.lockState = CursorLockMode.Confined;  // 마우스 이탈 방지
    }
    public void OnChatScreen(){ 
        ChatScreen.SetActive(true);
    }
    public void OffChatScreen(){  // using btn CloseBtn in ChatScreen
        ChatScreen.SetActive(false);
    }
    public void GamePause(){
        Time.timeScale = 0f;
        isPaused = true; // 게임이 일시정지 상태임을 표시
    }

    public void GameContinue(){  // using btn CloseBtn in ChatScreen
        Time.timeScale = 1f;
        isPaused = false; // 게임이 일시정지 상태가 아님을 표시
    }

    public void OnUIScreen()
    {
        UIScreen.SetActive(true);
    }

    public void FirstInvasion() // using Npc 
    {
        currentTimer = timerDuration; // 타이머를 초기화합니다
        if (firstInvade)
            StartCoroutine(MonsterInvasion(timerDuration));
        firstInvade = false;
    }

    IEnumerator MonsterInvasion(float timerDuration)
    {
        while (currentTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTimer -= 1f; // 매초 타이머를 감소시킵니다
        }

        foreach (GameObject npc in npcs)
        {
            npc.GetComponent<NPC>().GoShelter();
        }

        monsterSpwanManagerObj.GetComponent<MonsterSpawn>().StartStage();
    }

    public void UpdateRemainingMonster()
    {
        currentMonster--;
        remainingMonstersText.text = currentMonster.ToString();
        if (currentMonster == 0) EnterRestStage();
    }

    public void EnterStage()
    {
        foreach (GameObject npc in npcs)
        {
            npc.GetComponent<NPC>().GoShelter();
        }
        monsterSpwanManagerObj.GetComponent<MonsterSpawn>().StartStage();
    }

    public void EnterRestStage()
    {
        foreach (GameObject npc in npcs)
        {
            npc.GetComponent<NPC>().GoToOriginalPosition();
        }
        currentTimer = 60f;
        StartCoroutine(WaitAndStartNextStage(currentTimer)); // 60초 후에 다음 스테이지 시작
    }
    IEnumerator WaitAndStartNextStage(float waitTime)
    {
        while (currentTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTimer -= 1f; // 매초 타이머를 감소시킵니다
            restStageTimerText.text = currentTimer.ToString();
        }
        EnterStage(); // 예를 들어 EnterStage 메서드 호출
    }

    public void GameOver()
    {
        // 게임 종료 로직
        // 예: 게임 오버 화면 표시, 재시작 또는 메뉴로 돌아가는 등의 기능
        Debug.Log("Game Over! Treasure has been destroyed.");
    }
}