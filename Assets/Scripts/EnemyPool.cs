using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class EnemyPool : PoolManager
    {
        public static EnemyPool Instance;

        private void Awake()
        {
            if (EnemyPool.Instance == null)
            {
                EnemyPool.Instance = this;
                StartCoroutine(Initialize());
            }
            else if (EnemyPool.Instance != this)
            {
                Destroy(gameObject);
            }
        }

    }
}
