using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DigitalRuby.Tween {
    public class MoveUIElement : MonoBehaviour
    {
        [SerializeField] GameObject obj;
        [SerializeField]Vector3 initialPos;

        [SerializeField] Transform[] positions;
        public float duration;
        public float timeElapsed;
        [SerializeField] UnityEvent finishEvents;

        private bool isMoving = false;
        private Vector3 startPosition;
        private Vector3 finishPosition;

        // Start is called before the first frame update
        void Awake()
        {
            obj = this.gameObject;
            initialPos = transform.position;
        }

        /*public void MoveToPosition(int id)
        {
            System.Action<ITween<Vector3>> updateCirclePos = (t) =>
            {
                obj.gameObject.transform.position = t.CurrentValue;
            };

            obj.Tween(obj, transform.position, positions[id].position, duration, TweenScaleFunctions.Linear, updateCirclePos);
        }

        public void MoveToInitialPosition()
        {
            System.Action<ITween<Vector3>> updateCirclePos = (t) =>
            {
                obj.gameObject.transform.position = t.CurrentValue;
            };

            obj.Tween(obj, transform.position, initialPos, duration, TweenScaleFunctions.Linear, updateCirclePos);
            StartCoroutine(Events());
        }
        */

        private void Events()
        {
            //yield return new WaitForSeconds(duration);
            finishEvents.Invoke();
        }


        public void MoveToPosition(int id)
        {
            startPosition = obj.gameObject.transform.position;
            finishPosition = positions[id].transform.position;

            timeElapsed = 0;
            isMoving = true;
            //StartCoroutine(Events());
        }

        public void MoveToInitialPosition()
        {
            startPosition = obj.gameObject.transform.position;
            finishPosition = initialPos;

            timeElapsed = 0;
            isMoving = true;
            //StartCoroutine(Events());
        }

        private void Update()
        {
            if (isMoving)
            {
                Move();
            }
        }


        void Move()
        {
            // Increase timeElapsed based on the time passed each frame
            timeElapsed += Time.deltaTime;

            // Interpolate position based on the ratio of time elapsed and the total duration
            float t = timeElapsed / duration;

            // Lerp from startPosition to endPosition, smoothly over time
            obj.gameObject.transform.position = Vector3.Lerp(startPosition, finishPosition, t);

            // Optional: Stop the movement after the duration has passed
            if (timeElapsed >= duration)
            {
                Events();
                obj.gameObject.transform.position = finishPosition;
                isMoving = false;
            }
        }
    }
}
