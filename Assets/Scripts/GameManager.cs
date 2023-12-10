using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject ChatScreen;

    public bool IsPaused { get; private set; } = false;  // 게임의 일시정지 상태를 나타내는 변수

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
        IsPaused = true; // 게임이 일시정지 상태임을 표시
    }

    public void GameContinue(){  // using btn CloseBtn in ChatScreen
        Time.timeScale = 1f;
        IsPaused = false; // 게임이 일시정지 상태가 아님을 표시
    }

    public void OnUIScreen()
    {
        UIScreen.SetActive(true);
    }
}