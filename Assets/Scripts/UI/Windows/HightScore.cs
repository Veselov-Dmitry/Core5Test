using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Core5Test
{
    public class HightScore : WindowBase
    {
        public Action OnBack;

        private const string NAME_HIGHSCORE_SAVE = "HighScore";

        [SerializeField]
        private EnterName m_EnterName = null;
        [SerializeField]
        private HighScorePool m_HighScorePool = null;
        [SerializeField]
        private List<HighScoreItem> m_Rows = new List<HighScoreItem>();
        [SerializeField]
        private RectTransform m_ContentRectTransform = null;
        [SerializeField]
        private GridLayoutGroup m_ContentGridLayoutGroup = null;

        private List<ScoreRow> m_ScoreRows = new List<ScoreRow>();
        private int m_Score;

        public class ScoreRow
        {
            public string Name;
            public int Score;
        }

        public override void Close()
        {
            if (OnBack != null)
            {
                OnBack.Invoke();
            }
            base.Close();
        }
        public override void Open()
        {
            base.Open();
            if (m_Rows.Count > 0)
            {
                for (int i = m_Rows.Count - 1 ; i >= 0 ; i--)
                {
                    m_Rows[i].ReturnToPool();
                }
            }
            m_Rows.Clear();
            m_ScoreRows.Clear();
            string text = PlayerPrefs.GetString(NAME_HIGHSCORE_SAVE);
            int val = 0;
            if (string.IsNullOrWhiteSpace(text) == false)
            {
                var lines = text.Split('\n');
                if (lines != null && lines.Length > 0)
                {
                    for (int i = lines.Length - 1 ; i >= 0 ; i--)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i]))
                        {
                            continue;
                        }
                        var playerInfo = lines[i].Split('=');
                        if (playerInfo.Length == 2)
                        {
                            val = 0;
                            Int32.TryParse(playerInfo[1],out val);
                            ScoreRow sr = new ScoreRow() { Name = playerInfo[0], Score = val };
                            m_ScoreRows.Add(sr);
                        }
                    }
                }
            }
            BuildRows();
        }

        private void BuildRows()
        {
            int size = m_ScoreRows.Count;
            if (size > 1)
            {
                for (int i = 1 ; i < size ; i++)
                {
                    for (int j = 0 ; j < (size - i) ; j++)
                    {
                        if (m_ScoreRows[j].Score > m_ScoreRows[j + 1].Score)
                        {
                            var temp = m_ScoreRows[j];
                            m_ScoreRows[j] = m_ScoreRows[j + 1];
                            m_ScoreRows[j + 1] = temp;
                        }
                    }
                }
            }
            for (int i = 0 ; i < size ; i++)
            {
                HighScoreItem item = m_HighScorePool.GetItem() as HighScoreItem;
                item.transform.SetParent(m_HighScorePool.transform);
                item.gameObject.SetActive(true);
                item.Set(i+1, m_ScoreRows[i].Name, m_ScoreRows[i].Score);
                m_Rows.Add(item);
            }
            float contentSize = m_ContentGridLayoutGroup.cellSize.y * size;
            m_ContentRectTransform.offsetMin = new Vector2(0, -contentSize);
            m_ContentRectTransform.offsetMax = new Vector2(0, 0.01f);
        }

        public void AddNewPlayer(int score)
        {
            m_Score = score;
            m_EnterName.Open(score);
            m_EnterName.OnSave = CreateRow;
        }

        private void CreateRow(string name)
        {
            ScoreRow sr = new ScoreRow() { Name = name, Score = m_Score };
            m_ScoreRows.Add(sr);
            if (m_Rows.Count > 0)
            {
                for (int i = m_Rows.Count - 1 ; i >= 0 ; i--)
                {
                    m_Rows[i].ReturnToPool();
                }
            }
            m_Rows.Clear();
            BuildRows();
            m_EnterName.Close();
            SaveRows();
        }

        private void SaveRows()
        {
            StringBuilder sb = new StringBuilder();
            string separator = "=";
            for (int i = m_ScoreRows.Count - 1 ; i >= 0 ; i--)
            {
                sb.AppendLine(string.Join(separator, m_ScoreRows[i].Name, m_ScoreRows[i].Score.ToString()));
            }
            PlayerPrefs.SetString(NAME_HIGHSCORE_SAVE, sb.ToString());
        }

#if UNITY_EDITOR
        [ContextMenu("++TestFill")]
        public void TestFill()
        {
            List<ScoreRow> list = new List<ScoreRow>();
            list.Add(new ScoreRow() { Name = "John", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Will", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Ivan", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "George", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Bill", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Sergey", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "John", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Will", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Ivan", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "George", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Bill", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Sergey", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "John", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Will", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Ivan", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "George", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Bill", Score = Random.Range(1, 1000) });
            list.Add(new ScoreRow() { Name = "Sergey", Score = Random.Range(1, 1000) });
            m_ScoreRows = list;
            SaveRows();
            if (m_Rows.Count > 0)
            {
                for (int i = m_Rows.Count - 1 ; i >= 0 ; i--)
                {
                    m_Rows[i].ReturnToPool();
                }
            }
            m_Rows.Clear();
            BuildRows();
        }

        [ContextMenu("++TestClear")]
        public void TestClear()
        {
            List<ScoreRow> list = new List<ScoreRow>();
            m_ScoreRows = list;
            SaveRows();
            if (m_Rows.Count > 0)
            {
                for (int i = m_Rows.Count - 1 ; i >= 0 ; i--)
                {
                    m_Rows[i].ReturnToPool();
                }
            }
            m_Rows.Clear();
            BuildRows();
        }
        [ContextMenu("++TestAddNewPlayer")]
        public void TestAddNewPlayer()
        {
            AddNewPlayer(12345);
        }
#endif
    }
}
