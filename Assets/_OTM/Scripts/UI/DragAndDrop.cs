using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public int ID;
    private RectTransform rectTransform;
    //private Collider2D collider2D;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 originalPosition;
    [SerializeField] bool onDrag;


    public delegate void Collision (DragAndDrop gameObject);
    public static event Collision CollisionEvent;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        originalPosition = rectTransform.anchoredPosition;
        onDrag = false;

        Answer.IncorrectEvent += ReturnPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Optionally handle logic when dragging starts
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag = true;
        // Update the position of the UI element
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        rectTransform.localScale = Vector3.one / 2;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onDrag = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!onDrag && collision.tag != "DragObject")
        {
            Debug.Log(name + " Trigger activado con: " + collision.gameObject.name);
            Answer answer = collision.gameObject.GetComponent<Answer>();
            if (answer != null)
            {
                answer.ReactCollision(this);
            }

            else
                ReturnPosition();
        }

    }


    public void ReturnPosition()
    {
        rectTransform.localScale = Vector3.one;
        rectTransform.anchoredPosition = originalPosition;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta cuando el objeto arrastrado colisiona con otro
        Debug.Log("Colision� con: " + collision.gameObject.name);
    }
}