using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject StartScreen;

    public void OffStartScreen(){
        StartScreen.SetActive(false);
    }
    
}
