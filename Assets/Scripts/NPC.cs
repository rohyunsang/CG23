using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
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
            GameManager.Instance.OnChatScreen();
            GameManager.Instance.GamePause();
        }
    }
}