using UnityEngine.UI;
using UnityEngine;

namespace Core5Test
{
    public class EnterName : WindowBase
    {
        public System.Action<string> OnSave;

        [SerializeField]
        private Text m_Text = null;
        [SerializeField]
        private Text m_Score = null;
        [SerializeField]
        private Button m_Button = null;

        public void Open(int score)
        {
            m_Score.text = score.ToString();
            Open();
        }

        public void ValidateText()
        {
            if (m_Text.text.Length == 0)
            {
                m_Button.interactable = false;
            }
            else
            {
                m_Button.interactable = true;
            }
        }

        public void Save()
        {
            if (OnSave != null)
            {
                OnSave.Invoke(m_Text.text);
            }
        }
    }
}
