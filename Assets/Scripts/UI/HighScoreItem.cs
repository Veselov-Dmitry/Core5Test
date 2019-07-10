using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core5Test
{
    public class HighScoreItem : PoolItem
    {
        [SerializeField]
        private Text m_Text = null;

        public void Set(int position,string name, int score)
        {
            m_Text.text = String.Format("{0}.{1} - {2}", position.ToString(), name, score);
        }
        public override void ReturnToPool()
        {
            m_Text.text = "";
            transform.SetParent(null);
            base.ReturnToPool();
        }
    }
}
