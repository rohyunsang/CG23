using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �±װ� "Player"���� Ȯ��
        if (other.CompareTag("Player"))
        {
            // ���� ī��Ʈ ���� ����
            // ���� ���, ���� �Ŵ����� ���� ī��Ʈ�� �����ϴ� �ٸ� ��ũ��Ʈ�� �޼ҵ� ȣ��
            GameManager.Instance.coinCount++;
            // ���� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
