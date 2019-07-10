using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core5Test
{
    public class Movement : MonoBehaviour
    {
        private YieldInstruction wait = new WaitForEndOfFrame();
        private IEnumerator Rotating;
        private IEnumerator Moving;
        protected float RotationSpeed = 2f;

        protected virtual float MySpeed
        {
            get
            {
                return 1;
            }
        }

        protected void MovingAndRotateTo(Vector3 Target)
        {
            if (Rotating != null)
            {
                StopCoroutine(Rotating);
            }
            Rotating = RotatingCouroutine(Target);
            StartCoroutine(Rotating);
            if (Moving != null)
            {
                StopCoroutine(Moving);
            }
            Moving = MovingCouroutine(Target);
            StartCoroutine(Moving);
        }
        private IEnumerator RotatingCouroutine(Vector3 Target)
        {
            Vector3 direction = (Target - transform.position);
            float value = 0;
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);

            while (true)
            {
                if (value >= 1)
                {
                    transform.rotation = rotation;
                    break;
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, value);
                value += Time.deltaTime * RotationSpeed;
                yield return wait;
            }
            yield return null;
        }
        private IEnumerator MovingCouroutine(Vector3 finish)
        {
            Vector3 begin = transform.position;
            float path = (finish - transform.position).magnitude;
            float value = 0;
            Vector3 pos;
            while (true)
            {
                if (value >= path)
                {
                    value = path;
                    pos = Vector3.MoveTowards(begin, finish, value);
                    transform.position = pos;
                    break;
                }
                value += Time.deltaTime * MySpeed;
                pos = Vector3.MoveTowards(begin, finish, value);
                transform.position = pos;
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }
    }
}
