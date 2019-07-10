using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class InputController : MonoBehaviour
    {
        public static Action<Vector2> OnGetTouch;

        private Camera _cam;
        public Camera cam
        {
            get
            {
                if (_cam == null)
                {
                    _cam = Camera.main;
                }
                return _cam;
            }
        }

        void Update()
        {

            if (GameManager.GameState == GameState.GamePause)
            {
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (OnGetTouch != null)
                {
                    Vector3 mouse = ScreenToCenterScreen(Input.mousePosition);
                    OnGetTouch.Invoke(mouse);
                }
            }
        }



        private Vector3 ScreenToCenterScreen(Vector3 pos)
        {
            //pos = new Vector3(pos.x - Screen.width / 2, pos.y - Screen.height / 2, 0);
            pos = cam.ScreenToWorldPoint(pos);
            return pos;
        }
    }
}
