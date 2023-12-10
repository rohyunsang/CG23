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
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ�

    private string[] introTexts = new string[]
    {
        "ȯ���մϴ�, �����̽� ������. �� ����� ����� ������ �ղž� ��ٷȽ��ϴ�",
        "�̰��� ���� ��, ��� ���� ������ ��췯�� ȯ���� ���Դϴ�.",
        "�׷��� �ֱ�, ������ ���� ����� ���� ������� ��ȭ�� �����ϰ� �ֽ��ϴ�.",
        "�� ������ �ذ��� �� �ִ� ���� ����� ���� �������Դϴ�. �Բ� �� ���⸦ �Ѿ �ֽñ� �ٶ��ϴ�."
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
        StopAllCoroutines(); // ���� �ڷ�ƾ ����
        StartCoroutine(TypeText(introTexts[cur]));
    }

    IEnumerator TypeText(string textToType)
    {
        introText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        float startTime = Time.realtimeSinceStartup; // ���� �ð� ���

        foreach (char letter in textToType.ToCharArray())
        {
            introText.text += letter; // ���� �ϳ��� �߰�
            yield return new WaitUntil(() => Time.realtimeSinceStartup >= startTime + typingSpeed); // ���� ���ڰ� ��Ÿ���� �� ���
            startTime = Time.realtimeSinceStartup; // ���� ���� ��� �ð��� ���� ���� �ð� ������Ʈ
        }
    }

}
