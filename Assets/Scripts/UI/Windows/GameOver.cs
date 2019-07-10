using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class GameOver : WindowBase
    {
        public Action OnClose;

        public override void Open()
        {
            base.Open();
            StartCoroutine(WaitToClose());
        }

        private IEnumerator WaitToClose()
        {
            yield return new WaitForSeconds(2);
            if (OnClose != null)
            {
                OnClose.Invoke();
                Close();
            }
        }
    }
}
