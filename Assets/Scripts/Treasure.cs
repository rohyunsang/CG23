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
        currentHp = maxHp;    // ���� �� �ִ� HP�� ����
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
