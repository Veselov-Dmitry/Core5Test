using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class WindowBase : MonoBehaviour
    {
        private Canvas m_Canvas;
        protected Canvas Canvas
        {
            get
            {
                if (m_Canvas == null)
                {
                    m_Canvas = GetComponent<Canvas>();
                }
                return m_Canvas;
            }
        }
        public virtual void Close()
        {
            Canvas.enabled = false;
        }
        public virtual void Open()
        {
            Canvas.enabled = true;
        }
    }
}
