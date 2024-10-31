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
