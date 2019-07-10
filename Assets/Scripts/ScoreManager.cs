using UnityEngine;
using UnityEngine.UI;

namespace Core5Test
{
    public class ScoreManager : MonoBehaviour
    {
        public int m_Score = 0;
        public int Score
        {
            get
            {
                return m_Score;
            }
            private set
            {
                m_Score = value;
                m_ScoreText.text = m_Score.ToString();
            }
        }

        [SerializeField]
        private Text m_ScoreText = null;

        public void ResetScore()
        {
            Score = 0;
        }

        public void AppenScore(int value)
        {
            Score += value;
        }
    }
}
