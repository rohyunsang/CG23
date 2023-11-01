using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        GameManager.Instance.OffChatScreen();
    }

    void OnTriggerStay(Collider other){
        if(Input.GetKeyDown(KeyCode.C))
            GameManager.Instance.OnChatScreen();
    }

}