using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject introScreen;
    public Text introText;
    int cur = 0;
    public float typingSpeed = 0.05f; // 타이핑 속도

    private string[] introTexts = new string[]
    {
        "환영합니다, 영웅이신 여러분. 이 세계는 당신의 도래를 손꼽아 기다렸습니다",
        "이곳은 숲과 들, 산과 강이 마법과 어우러진 환상의 땅입니다.",
        "그러나 최근, 차원의 문을 통과한 낯선 존재들이 평화를 위협하고 있습니다.",
        "이 난제를 해결할 수 있는 것은 당신의 용기와 지혜뿐입니다. 함께 이 위기를 넘어서 주시길 바랍니다."
    };
    private void Start()
    {
        GameManager.Instance.GamePause();
    }

    public void OnClickStartButton() // using button Start Btn in StartScreen
    {
        startScreen.SetActive(false);
        introScreen.SetActive(true);
        UpdateIntroText();

    }

    public void OnClickNextButton()
    {
        cur++;
        if (cur >= introTexts.Length)
        {
            introScreen.SetActive(false);
            GameManager.Instance.GameContinue();
            GameManager.Instance.OnUIScreen();
            cur = 0; // Reset cur or handle as needed 
        }
        else
        {
            UpdateIntroText();
        }
    }

    private void UpdateIntroText()
    {
        StopAllCoroutines(); // 이전 코루틴 중지
        StartCoroutine(TypeText(introTexts[cur]));
    }

    IEnumerator TypeText(string textToType)
    {
        introText.text = ""; // 텍스트 초기화
        float startTime = Time.realtimeSinceStartup; // 시작 시간 기록

        foreach (char letter in textToType.ToCharArray())
        {
            introText.text += letter; // 글자 하나씩 추가
            yield return new WaitUntil(() => Time.realtimeSinceStartup >= startTime + typingSpeed); // 다음 글자가 나타나기 전 대기
            startTime = Time.realtimeSinceStartup; // 다음 글자 대기 시간을 위해 현재 시간 업데이트
        }
    }

}
