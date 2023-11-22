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
        StartCoroutine(PointAnimation());
    }

    public void UpdateHealth()
    {
        health--;
        if(health == 2)
        {
            heart3.GetComponent<CanvasRenderer>().SetColor(color:Color.black);
            StartCoroutine(HeartAnimation(heart3));
        }
        if(health == 1)
        {
            heart2.GetComponent<CanvasRenderer>().SetColor(color: Color.black);
            StartCoroutine(HeartAnimation(heart2));
        }
        if (health == 0)
        {
            heart1.GetComponent<CanvasRenderer>().SetColor(color: Color.black);
            StartCoroutine(HeartAnimation(heart1));
        }
    }

    private IEnumerator HeartAnimation(GameObject heart)
    {
        for(int i = 0; i<5; i++)
        {
            heart.SetActive(!heart.activeSelf);
            yield return new WaitForSeconds(0.1f);
        }
        GameObject fadingHeart = Instantiate(heart, heart.transform.position, Quaternion.identity, transform.parent);
        fadingHeart.SetActive(true);
    }

    private IEnumerator PointAnimation()
    {
        float duration = 1.5f;
        float maxSize = 1.9f;
        float timer = 0f;
        while(timer<duration * 0.2f)
        {
            Debug.Log("score animating");
            timer +=Time.deltaTime;
            float scale = Mathf.Lerp(1f, maxSize, timer / (duration * 0.2f));
            score.transform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }
        score.transform.localScale = Vector3.one;
    }
}
