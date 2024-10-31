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
        [SerializeField] UnityEvent finishEvents;

        // Start is called before the first frame update
        void Awake()
        {
            obj = this.gameObject;
            initialPos = transform.position;
        }

        public void MoveToPosition(int id)
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

        IEnumerator Events()
        {
            yield return new WaitForSeconds(duration);
            finishEvents.Invoke();
        }
    }
}
