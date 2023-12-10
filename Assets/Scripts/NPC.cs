using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public class NPC : MonoBehaviour
{
    public GameObject gptManager;
    public string characterConcept = "";

    void Update() 
    {
        // 게임이 일시정지 상태일 때만 ESC 키를 감지합니다.
        if (GameManager.Instance.IsPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.GameContinue();
            GameManager.Instance.OffChatScreen();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            gptManager.GetComponent<ChatGPT>().characterConcept = characterConcept;
            GameManager.Instance.OnChatScreen();
            GameManager.Instance.GamePause();
        }
    }
}