using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class BonusBase : MonoBehaviour
    {
        [SerializeField]
        protected float m_TimeBooster = 10f;
        [SerializeField]
        private PlayerMovement m_PlayerMovement = null;
        protected PlayerMovement PlayerMovement
        {
            get
            {
                if (m_PlayerMovement == null)
                {
                    m_PlayerMovement = GetComponent<PlayerMovement>();
                }
                return m_PlayerMovement;
            }
        }
        [SerializeField]
        private Player m_Player;
        protected Player Player
        {
            get
            {
                if (m_Player == null)
                {
                    m_Player = GetComponent<Player>();
                }
                return m_Player;
            }
        }
        private IEnumerator m_LifeTime;

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        public virtual void ReActivate(float value, float time)
        {
            return;
        }

        public virtual void Activate(float value, float time)
        {
            m_TimeBooster = time;
            if (m_LifeTime != null)
            {
                StopCoroutine(m_LifeTime);
            }
            m_LifeTime = LifeTimeCoroutine();
            StartCoroutine(m_LifeTime);
        }
        protected virtual void DeActivate()
        {
            return;
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return new WaitForSeconds(m_TimeBooster);
            DeActivate();
            Destroy(this);
        }
    }
}
