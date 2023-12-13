using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            // 코인 카운트 증가 로직
            // 예를 들어, 게임 매니저나 코인 카운트를 관리하는 다른 스크립트의 메소드 호출
            GameManager.Instance.coinCount++;
            // 코인 오브젝트 제거
            Destroy(gameObject);
        }
    }
}
