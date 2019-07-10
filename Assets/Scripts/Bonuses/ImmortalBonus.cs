using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class ImmortalBonus : BonusBase
    {
        [SerializeField]
        private GameObject m_ImmortalFX;

        protected override void DeActivate()
        {
            Destroy(m_ImmortalFX);
        }
        public override void ReActivate(float value, float time)
        {
            base.Activate(value, time);
        }

        public override void Activate(float value, float time)
        {
            m_ImmortalFX = Instantiate(Player.ImmortalFXPrefab, transform);
            base.Activate(value, time);
        }
    }
}
