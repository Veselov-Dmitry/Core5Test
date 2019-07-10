using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        protected List<PoolItem> m_Items = new List<PoolItem>();
        [SerializeField]
        private Transform m_HidePosition = null;
        [SerializeField]
        private int m_Size = 10;
        [SerializeField]
        private PoolItem m_PooledObject = null;

        protected IEnumerator Initialize()
        {
            for (int i = 0 ; i < m_Size ; i++)
            {
                PoolItem item = Instantiate(m_PooledObject,transform);
                item.gameObject.name = "PoolObject " + i.ToString("00");
                item.PoolManager = this;
                ReturnItem(item);
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }

        public void ReturnItem(PoolItem poolItem)
        {
            if (m_Items.Contains(poolItem) == false)
            {
                m_Items.Add(poolItem);
            }
            poolItem.gameObject.SetActive(false);
            poolItem.gameObject.transform.position = m_HidePosition.position;
        }

        public PoolItem GetItem()
        {
            PoolItem item = null;
            for (int i = m_Items.Count - 1 ; i >= 0 ; i--)
            {
                if (m_Items[i].gameObject.activeSelf == false)
                {
                    item = m_Items[i];
                    break;
                }
            }
            if (item == null)
            {
                item = Instantiate(m_PooledObject, transform);
                item.PoolManager = this;
                ReturnItem(item);
                item.gameObject.name = "PoolObject " + m_Items.IndexOf(item).ToString("00");
            }
            return item;
        }
        public void ReturnAllItems()
        {
            for (int i = m_Items.Count - 1 ; i >= 0 ; i--)
            {
                if (m_Items[i].gameObject.activeSelf)
                {
                    m_Items[i].ReturnToPool();
                }
            }
        }
    }
}
