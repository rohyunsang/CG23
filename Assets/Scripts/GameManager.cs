using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject ChatScreen;

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
    }

    public void OnChatScreen(){
        ChatScreen.SetActive(true);
    }
    public void OffChatScreen(){
        ChatScreen.SetActive(false);
    }

    void Start()
    {
        UIScreen.SetActive(true);
    }
}