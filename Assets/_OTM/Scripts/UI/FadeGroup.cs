using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DigitalRuby.Tween
{
    public class FadeGroup : MonoBehaviour
    {
        [SerializeField] bool fadeOnEnable = false;
        CanvasGroup group;
        public float duration;
        [SerializeField] UnityEvent eventHide;
        [SerializeField] UnityEvent eventShow;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            if (fadeOnEnable)
            {
                FadeIn();
            }
        }

        public void FadeIn()
        {
            if (group == null)
            {
                group = GetComponent<CanvasGroup>();
            }

            float tempColor = group.alpha;
            tempColor = 0;
            group.alpha = tempColor;

            System.Action<ITween<float>> updateCirclePos = (t) =>
            {
                float tempColor = group.alpha;
                tempColor = t.CurrentValue;
                group.alpha = tempColor;
            };

            group.gameObject.Tween(group.gameObject, 0, 1, duration, TweenScaleFunctions.Linear, updateCirclePos);
            StartCoroutine(Events(true));
        }

        public void FadeOut()
        {
            if (group == null)
            {
                group = GetComponent<CanvasGroup>();
            }

            float tempColor = group.alpha;
            tempColor = 1;
            group.alpha = tempColor;

            System.Action<ITween<float>> updateCirclePos = (t) =>
            {
                float tempColor = group.alpha;
                tempColor = t.CurrentValue;
                group.alpha = tempColor;
            };

            group.gameObject.Tween(group, 1, 0, duration, TweenScaleFunctions.Linear, updateCirclePos);
            StartCoroutine(Events(false));
        }

        private IEnumerator Events(bool isShowing)
        {
            yield return new WaitForSeconds(duration);

            if (isShowing)
            {
                eventShow.Invoke();
            }
            else
            {
                eventHide.Invoke();
            }
        }

        public void ChangeDuration(float d)
        {
            duration = d;
        }
    }
}
