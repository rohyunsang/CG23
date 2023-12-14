using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject ShopScreen;
    public static Panel Instance { get; private set; }
    public void OffStartScreen(){
        StartScreen.SetActive(false);
    }
    public void OffShopScreen() // using btn in ShopScreen
    {
        ShopScreen.SetActive(false);
    }
    public void OnShopScreen()
    {
        ShopScreen.SetActive(true);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents the object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this one
        }
    }
}
