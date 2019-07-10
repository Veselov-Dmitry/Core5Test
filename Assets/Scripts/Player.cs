using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class Player : MonoBehaviour
    {
        public static System.Action<Bonus> OnFindBonus;

        public bool Immortal
        {
            get
            {
                return GetComponent<ImmortalBonus>();
            }
        }
        [SerializeField]
        private PlayerMovement m_PlayerMovement = null;
        [SerializeField]
        private Animator m_ImmortalFXPrefab = null;
        [SerializeField]
        private TrailRenderer m_SpeedFXPrefab = null;

        public GameObject ImmortalFXPrefab { get { return m_ImmortalFXPrefab.gameObject; } }
        public GameObject SpeedFXPrefab { get { return m_SpeedFXPrefab.gameObject; } }

        private void OnCol(GameObject go)
        {
            //print("Enemy OnTrigger find: " + go.name);
            var bonus = go.GetComponent<Bonus>();
            if (bonus != null)
            {
                if (OnFindBonus != null)
                {
                    OnFindBonus.Invoke(bonus);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCol(collision.gameObject);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            OnCol(collision.gameObject);
        }

        public void SetSpeed(float speed)
        {
            m_PlayerMovement.SetSpeed(speed);
        }
    }
}
