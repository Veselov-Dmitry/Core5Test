using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class PlayerMovement : Movement
    {

        private void OnEnable()
        {
            InputController.OnGetTouch += InputController_OnGetTouch;
        }
        private void OnDisable()
        {
            InputController.OnGetTouch -= InputController_OnGetTouch;
        }

        protected override float MySpeed
        {
            get
            {
                return Speed;
            }
        }

        public float Speed { get; private set; }

        public void SetSpeed(float value)
        {
            Speed = value;
        }

        private void InputController_OnGetTouch(Vector2 touchPosition)
        {
            MovingAndRotateTo(touchPosition);
        }

    }
}
