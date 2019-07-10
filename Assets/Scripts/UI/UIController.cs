using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameOver m_GameOver = null;
        [SerializeField] private HightScore m_HightScore = null;
        [SerializeField] private StartScreen m_StartScreen = null;
        [SerializeField] private FinishScreen m_FinishScreen = null;

        [SerializeField]
        private TimeCounter m_TimeCounter = null;
        [SerializeField]
        private ScoreManager m_ScoreManager = null;

        private void Start()
        {
            m_StartScreen.Open();
        }

        public void GameOwer()
        {
            m_TimeCounter.Stop();
            m_GameOver.Open();
            m_GameOver.OnClose = ShowHighScoreAddNew;
            m_HightScore.OnBack = ShowFinishScreen;
        }
        public void ShowFinishScreen()
        {
            m_FinishScreen.Open();
        }
        public void ShowHighScoreAddNew()
        {
            m_HightScore.Open();
            m_HightScore.AddNewPlayer(m_ScoreManager.Score);
        }
        public void ShowHighScore()
        {
            m_HightScore.Open();
            m_HightScore.OnBack = HideHighScore;
        }
        public void HideHighScore()
        {
            m_StartScreen.Open();
        }

        public void StartTime()
        {
            m_TimeCounter.StartTime();
        }
    }
}
