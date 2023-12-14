using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();

        public string saveMsg = "";
        public string characterConcept = "";
        private string prompt = "30단어 이하로 답변한다." 
                                + "한국어로 답변";

        public string npcDialogues = "";
        public void SetCharacterConcept(string s)
        {
            characterConcept = s;
        }


        #region Diary GPT
        public async void SaveToDiary()
        {
            Debug.Log("Start SaveToDiary");

            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = "1. 다음 대화를 일기 형식으로 변환해주세요 2. 판타지 어투를 사용해주세요 3. 한국어로 답변해주세요 4. 50글자 이내로 답변해주세요 : \n" + npcDialogues
            };

            messages.Add(newMessage);

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                Debug.Log("diaryEntry");
                // 변환된 일기 내용을 diarys 리스트에 추가
                GameManager.Instance.diarys.Add(message.Content);
                GameManager.Instance.InitDiary();
            }
            else
            {
                Debug.LogWarning("No diary entry was generated from this dialogue.");
            }

            npcDialogues = ""; // initialize
        }
        #endregion

        public static ChatGPT Instance { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            characterConcept = "";
        }

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = saveMsg + characterConcept + prompt + "\n" + inputField.text;
            if (inputField.text.Contains("상점") || inputField.text.Contains("구매")) Panel.Instance.OnShopScreen();
            npcDialogues += "User: " + inputField.text;

            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                npcDialogues += "NPC: " + message.Content; // 저장할 메시지 추가: GPT 응답

                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
