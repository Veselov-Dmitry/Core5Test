using System;
using UnityEngine;

namespace Core5Test
{
    public class PoolItem : MonoBehaviour
    {
        public PoolManager PoolManager;

        public virtual void ReturnToPool()
        {
            PoolManager.ReturnItem(this);
        }
    }
}
