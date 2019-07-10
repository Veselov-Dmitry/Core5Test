using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core5Test
{
    public class ScoreDigits : MonoBehaviour
    {
        [SerializeField]
        private Text m_Text = null;
        [SerializeField]
        private Transform m_Transform = null;
        [SerializeField]
        private Animator m_Animator = null;
        [SerializeField]
        private Canvas m_Canvas = null;

        public void Play(string value, Vector2 position)
        {
            m_Canvas.enabled = true;
            m_Transform.position = position;
            m_Animator.SetTrigger("Play");
            m_Text.text = "+"+ value;
        }


        public void Hide()
        {
            m_Canvas.enabled = false;
            m_Text.text = string.Empty;
        }
    }
}
