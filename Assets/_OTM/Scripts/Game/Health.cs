using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int life;
    private int actLife;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] List<GameObject> lifes;


    public void IncorrectAnswer()
    {
        actLife--;
        if (actLife != 0)
        {
            lifes[actLife].SetActive(false);
        }

        else
        {
            lifes[0].SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        actLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
