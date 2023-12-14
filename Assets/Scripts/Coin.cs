using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // Calculate the rotation amount
        float rotationAmount = 60 * Time.deltaTime;

        // Rotate around the y-axis
        transform.Rotate(0, rotationAmount, 0);
    }
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
