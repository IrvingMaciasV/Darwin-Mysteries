using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using DG.Tweening;

public class MoveElement : MonoBehaviour
{
    public float vel;
    public RectTransform initPos;
    public RectTransform[] positions;
    private int id = 0;
    [SerializeField] UnityEvent eventsOnMove;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    IEnumerator MoveToPos()
    {
        //Vector2 position = rectTransform.anchoredPosition;

        //position.x += vel * Time.deltaTime;

        //if (position.x > positions[id].)
        //    position.x = MinHorizontalPosition;

        //rectTransform.anchoredPosition = position;

        yield return null;
    }

    private void Update()
    {
        //Vector2 position = rectTransform.anchoredPosition;

        //position.x += HorizontalSpeed * Time.deltaTime;

        //if (position.x > MaxHorizontalPosition)
        //    position.x = MinHorizontalPosition;

        //rectTransform.anchoredPosition = position;
    }

    public void CloseSidePanel()
    {
        //this.transform.DOLocalMove(OriginalPos - new Vector3(1040, 0, 0), .5f);
        //this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //this.gameObject.GetComponent<CanvasGroup>().interactable = false;
        //isOpened = false;
    }
}
