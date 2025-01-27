using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DigitalRuby.Tween
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] bool fadeOnEnable = false;
        Image img;
        public float duration;
        [SerializeField] UnityEvent eventHide;
        [SerializeField] UnityEvent eventShow;

        private float timeElapsed;
        private bool isFading = false;
        private bool isFadingIn;

        private void Awake()
        {
            img = GetComponent<Image>();
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
            if (img == null)
            {
                img = GetComponent<Image>();
            }

            Color tempColor = img.color;
            tempColor.a = 0;
            img.color = tempColor;

            System.Action<ITween<float>> updateCirclePos = (t) =>
            {
                Color tempColor = img.color;
                tempColor.a = t.CurrentValue;
                img.color = tempColor;
            };

            img.gameObject.Tween(img.gameObject, 0, 1, duration, TweenScaleFunctions.Linear, updateCirclePos);
            StartCoroutine(Events(true));
        }

        public void FadeOut()
        {
            if (img == null)
            {
                img = GetComponent<Image>();
            }

            Color tempColor = img.color;
            tempColor.a = 1;
            img.color = tempColor;

            System.Action<ITween<float>> updateCirclePos = (t) =>
            {
                Color tempColor = img.color;
                tempColor.a = t.CurrentValue;
                img.color = tempColor;
            };

            img.gameObject.Tween(img, 1, 0, duration, TweenScaleFunctions.Linear, updateCirclePos);
            StartCoroutine(Events(false));
        }
        */

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

        public void ChangeDuration(float d)
        {
            duration = d;
        }

        public void FadeIn()
        {
            isFading = true;
            isFadingIn = true;
            timeElapsed = 0;

            Color tempColor = img.color;
            tempColor.a = 0;
            img.color = tempColor;
        }

        public void FadeOut()
        {
            isFading = true;
            isFadingIn = false;
            timeElapsed = 0;

            Color tempColor = img.color;
            tempColor.a = 1;
            img.color = tempColor;
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
                Color tempColor = img.color;
                tempColor.a = Mathf.Lerp(0f, 1f, t);
                img.color = tempColor;
            }

            else
            {
                Color tempColor = img.color;
                tempColor.a = Mathf.Lerp(1f, 0f, t);
                img.color = tempColor;
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
