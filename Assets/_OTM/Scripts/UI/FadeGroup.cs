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

        private float timeElapsed;
        private bool isFading = false;
        private bool isFadingIn;

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

        /*
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
        */

        public void ChangeDuration(float d)
        {
            duration = d;
        }



        private void Events(bool isShowing)
        {

            if (isShowing)
            {
                eventShow.Invoke();
            }
            else
            {
                eventHide.Invoke();
            }
        }

        public void FadeIn()
        {
            isFading = true;
            isFadingIn = true;
            timeElapsed = 0;

            group.alpha = 0;
        }

        public void FadeOut()
        {
            isFading = true;
            isFadingIn = false;
            timeElapsed = 0;

            group.alpha = 1;
        }

        private void Update()
        {
            if (isFading)
            {
                Fading();
            }
        }


        void Fading()
        {
            timeElapsed += Time.deltaTime;

            // Calculate interpolation factor (t)
            float t = timeElapsed / duration;

            if (isFadingIn)
            {
                group.alpha = Mathf.Lerp(0f, 1f, t);
            }

            else
            {
                group.alpha = Mathf.Lerp(1f, 0f, t);
            }

            // Optional: Disable script once fade is complete
            if (timeElapsed >= duration)
            {
                isFading = false;
                Events(isFadingIn);
            }
        }
    }
}
