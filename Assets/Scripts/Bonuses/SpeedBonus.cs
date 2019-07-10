using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class SpeedBonus : BonusBase
    {
        [SerializeField]
        private GameObject m_SpeedFX;
        [SerializeField]
        private float m_SpeedBooster = 2f;
        private float m_PlayerSpeedRemember;

        protected override void DeActivate()
        {
            Destroy(m_SpeedFX);
            PlayerMovement.SetSpeed(m_PlayerSpeedRemember / m_SpeedBooster);
        }
        public override void ReActivate(float value, float time)
        {
            base.Activate(value, time);
        }

        public override void Activate(float value, float time)
        {

            m_SpeedFX = Instantiate(Player.SpeedFXPrefab, transform);
            m_SpeedBooster = value;
            m_PlayerSpeedRemember = PlayerMovement.Speed;
            PlayerMovement.SetSpeed(m_PlayerSpeedRemember * m_SpeedBooster);
            base.Activate(value, time);
        }
    }
}
