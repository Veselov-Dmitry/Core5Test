using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class BonusPool : PoolManager
    {
        public static BonusPool Instance;

        private void Awake()
        {
            if (BonusPool.Instance == null)
            {
                BonusPool.Instance = this;
                StartCoroutine(Initialize());
            }
            else if (BonusPool.Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
