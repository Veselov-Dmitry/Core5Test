using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class Enemy : PoolItem
    {
        public static System.Action<Enemy> OnFindPlayer;

        private void OnCol(GameObject go)
        {
            //print("Enemy OnTrigger find: " + go.name);
            var player = go.GetComponent<Player>();
            if (player != null)
            {
                if (OnFindPlayer != null)
                {
                    OnFindPlayer.Invoke(this);
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

        public void Die()
        {
            ReturnToPool();
        }

    }
}
