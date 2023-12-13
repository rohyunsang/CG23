using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;
    public Slider slider;
    public float hpFillAmount;   // slider property value

    private void Start()
    {
        currentHp = maxHp;    // 시작 시 최대 HP로 설정
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("MonsterWeapon"))
        {
            currentHp -= col.GetComponent<MonsterWeapon>().attackDamage;
            Debug.Log("player hp " + currentHp);

            hpFillAmount = (float)currentHp / (float)maxHp;     // HP Bar
            slider.value = hpFillAmount;

            if (currentHp <= 0) GameManager.Instance.GameOver();
        }
    }

}
