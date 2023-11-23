using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI score, panelHighScore, panelYourScore, panelLongestStreak, streakText;
    public GameObject heart1, heart2, heart3, gameOverPanel, streakObj;
    private int health = 3, highScore = 15, localscore = 0;
    public bool isGameOver = false;
    GameController GameConRef;
    private Coroutine colorCycleCoroutine;


    private void Start()
    {
        GameConRef = GetComponent<GameController>();
        colorCycleCoroutine = StartCoroutine(SmoothCycleStreakTextColor());
    }
    public void UpdateScore(int newScore)
    {
        score.text = newScore.ToString();
        StartCoroutine(PointAnimation());
        localscore = newScore;
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(0);
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
            FinishGame();
        }
    }
    private void FinishGame()
    {
        gameOverPanel.SetActive(true);
        isGameOver = true;
        panelYourScore.text = score.text;
        if(localscore> highScore)
        {
            highScore = localscore;
            panelHighScore.text = highScore.ToString();
        }
        panelLongestStreak.text = GameConRef.currentLongestStreak.ToString();
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

    private IEnumerator SmoothCycleStreakTextColor()
    {
        Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };
        int currentColorIndex = 0;

        while (true)
        {
            Color startColor = colors[currentColorIndex];
            Color endColor = colors[(currentColorIndex + 1) % colors.Length];
            float transitionTime = 0f;
            float duration = 0.3f; // Duration for each color transition

            while (transitionTime < duration)
            {
                streakText.color = Color.Lerp(startColor, endColor, transitionTime / duration);
                transitionTime += Time.deltaTime;
                yield return null;
            }

            currentColorIndex = (currentColorIndex + 1) % colors.Length;
        }
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
