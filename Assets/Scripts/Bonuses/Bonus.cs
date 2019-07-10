using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class Bonus : PoolItem
    {
        [SerializeField]
        private BonusType m_Type = null;

        public BonusType Type
        {
            get
            {
                return m_Type;
            }
            private set
            {
                m_Type = value;
                m_SpriteRenderer.color = m_Type.GetColor();
            }
        }

        [SerializeField]
        private SpriteRenderer m_SpriteRenderer = null;

        public void Set(BonusType type)
        {
            Type = type;
        }

        public void Hide()
        {
            ReturnToPool();
        }

    }
}
