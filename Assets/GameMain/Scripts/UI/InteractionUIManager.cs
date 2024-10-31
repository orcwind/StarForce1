using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class InteractionUIManager : MonoBehaviour
    {
       // private const int InteractionPromptFormId = 100001; // 假设这是交互提示UI的ID

        public static InteractionUIManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void ShowInteractionPrompt(string promptText)
        {
            GameEntry.UI.OpenUIForm(UIFormId.UIInteractionPrompt, promptText);
        }

        public void HideInteractionPrompt()
        {
            UGuiForm uiForm = GameEntry.UI.GetUIForm(UIFormId.UIInteractionPrompt) as UGuiForm;
            if (uiForm != null)
            {
                GameEntry.UI.CloseUIForm(uiForm);
            }
        }
    }
}
