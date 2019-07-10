using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core5Test
{
    public class TimeCounter : MonoBehaviour
    {
        public static Action OnStepOver;

        public float m_StepValue;
        public float StepValue
        {
            get
            {
                return m_StepValue;
            }
            private set
            {
                m_StepValue = value;
            }
        }

        [SerializeField]
        private Text m_Text = null;
        [SerializeField]
        private float m_UpdateText = 0.33f;

        private IEnumerator m_TextUpdater;
        private float prevRest;
        private float curRest;
        private float value = 0;
        private float prevValue = 0;
        private YieldInstruction wait;
        private TimeSpan interval;

        public void Stop()
        {
            if (m_TextUpdater != null)
            {
                StopCoroutine(m_TextUpdater);
            }
        }

        public void StartTime()
        {
            prevValue = value = 0;
            StepValue = GameManager.Instance.StepTime;
            wait = new WaitForSeconds(m_UpdateText);
            if (m_TextUpdater != null)
            {
                StopCoroutine(m_TextUpdater);
            }
            m_TextUpdater = TextUpdaterCoroutine();
            StartCoroutine(m_TextUpdater);
        }

        private void Update()
        {
            if (GameManager.GameState == GameState.GamePause)
            {
                return;
            }
            value += Time.deltaTime;
            prevRest = prevValue % StepValue;
            curRest = value % StepValue;
            if (prevRest > curRest)
            {
                if (OnStepOver != null)
                {
                    OnStepOver.Invoke();
                }
            }
            prevValue = value;
        }


        private IEnumerator TextUpdaterCoroutine()
        {
            while (true)
            {
                interval = TimeSpan.FromSeconds(value);
                m_Text.text = string.Format("{0}:{1}",  Math.Floor(interval.TotalMinutes), interval.Seconds.ToString("00"));
                yield return wait;
            }
        }
    }
}
