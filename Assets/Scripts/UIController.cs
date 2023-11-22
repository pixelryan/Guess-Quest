using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject heart1, heart2, heart3;
    private int health = 3;

    public void UpdateScore(int newScore)
    {
        score.text = newScore.ToString();
    }

    public void UpdateHealth()
    {
        health--;
        if(health == 2)
        {
            heart3.GetComponent<CanvasRenderer>().SetColor(color:Color.black);
        }
        if(health == 1)
        {
            heart2.GetComponent<CanvasRenderer>().SetColor(color: Color.black);
        }
        if (health == 0)
        {
            heart1.GetComponent<CanvasRenderer>().SetColor(color: Color.black);
        }
    }
}
