using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class EnemyMovement : Movement
    {
        private YieldInstruction waitUpdate = new WaitForSeconds(0.2f);
        private Vector3 m_TargetPosition;
        private Transform m_Target;
        private Transform Target
        {
            get
            {
                if (m_Target == null)
                {
                    m_Target = GameManager.Instance.GetPlayer;
                }
                return m_Target;
            }
        }
        private float m_MySpeed = 0;
        protected override float MySpeed
        {
            get
            {
                return m_MySpeed;
            }
        }

        private void OnEnable()
        {
            m_TargetPosition = new Vector3(float.MaxValue,0,0);

            RotationSpeed = 1f;
            StarMovement();
        }
        private void OnDisable()
        {
            if (UpdateTarget != null)
            {
                StopCoroutine(UpdateTarget);
            }
        }

        public void SetSpeed(float speed)
        {
            m_MySpeed = speed;
        }
        [ContextMenu("++StarMovement")]
        public void StarMovement()
        {
            if (UpdateTarget != null)
            {
                StopCoroutine(UpdateTarget);
            }
            UpdateTarget = UpdateTargetCoroutine();
            StartCoroutine(UpdateTarget);
        }
        private IEnumerator UpdateTarget;
        private IEnumerator UpdateTargetCoroutine()
        {
            while (true)
            {
                if (GameManager.GameState == GameState.GamePause)
                {
                    yield return waitUpdate;
                }
                if (m_TargetPosition != Target.position)
                {
                    m_TargetPosition = Target.position;
                    MovingAndRotateTo(m_TargetPosition);
                }
                yield return waitUpdate;
            }
        }
    }
}
