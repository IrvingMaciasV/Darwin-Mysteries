using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Image imgSprite;
    private Sprite originalSprite;

    // Start is called before the first frame update
    void Awake()
    {
        imgSprite = gameObject.GetComponent<Image>();
        originalSprite = imgSprite.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(Sprite sprite)
    {
        imgSprite.sprite = sprite;
    }

    public void SetIddle()
    {
        imgSprite.sprite = originalSprite;
    }
}
