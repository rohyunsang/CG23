using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using OpenAI;

public class GameManager : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject ChatScreen;
    public GameObject GameOverScreen;
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

    public List<string> diarys = new List<string>();
    private int diaryIndex = 0;
    public Text diaryText;

    public GameObject chatGPTManager;

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
        chatGPTManager.GetComponent<ChatGPT>().SaveToDiary();
        
        
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
    public void InitDiary()
    {
        diaryText.text = diarys[diaryIndex];
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
    }
    public void ReloadScene() // using btn in GameOverScreen
    {
        SceneManager.LoadScene("Main");
    }
    public void OnclickLeftButton() // using btn
    {
        // 인덱스 감소
        if (diaryIndex > 0)
        {
            diaryIndex--;
            UpdateDiaryText();
        }
    }
    public void OnclickRightButton() // using btn
    {
        // 인덱스 증가
        if (diaryIndex < diarys.Count - 1)
        {
            diaryIndex++;
            UpdateDiaryText();
        }
    }

    private void UpdateDiaryText()
    {
        if (diaryText != null && diarys.Count > diaryIndex)
        {
            diaryText.text = diarys[diaryIndex];
        }
    }

}