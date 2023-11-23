using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab, cardSpawnLocator;
    public Deck deck;
    private GameObject lastCardDrawn;
    private Card lastCardDrawnInfo;
    public int Score, Lives, currentStreak, currentLongestStreak;
    private bool lastBet, firstDraw = true, isAnimating;
    private UIController UIConRef = null;
    public AudioClip goodSound, badSound, gameOverSound;
    public AudioClip[] cardSlide;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentStreak = 0;
        currentLongestStreak = 0;
        UIConRef = GetComponent<UIController>();
        deck = new Deck();
        DealHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HigherButtonPress()
    {
        if (isAnimating)
        {
            return;
        }
        firstDraw = false;
        Debug.Log("Higher Pressed");
        lastBet = true;
        DealHand();
        isAnimating = true;
    }

    public void LowerButtonPress()
    {
        if (isAnimating)
        {
            return;
        }
        firstDraw = false;
        Debug.Log("Lower Pressed");
        lastBet = false;
        DealHand();
        isAnimating = true;
    }

    public void DealHand()
    {
        audioSource.PlayOneShot(cardSlide[Random.Range(0, cardSlide.Count())]);
        deck.ShuffleDeck();
        Card drawnCard = deck.DrawCard();

        if (drawnCard != null)
        {
            Vector3 offScreenPos = new Vector3(-10, cardSpawnLocator.transform.position.y, cardSpawnLocator.transform.position.z);
            Quaternion initialRot = Quaternion.Euler(0, 0, 90);
            GameObject cardObject = Instantiate(cardPrefab, offScreenPos, initialRot, cardSpawnLocator.transform);
            CardController cardController = cardObject.GetComponent<CardController>();
            cardController.SetUpNewCard(drawnCard);

            // If there's a last card, pass it to the coroutine to be used for comparison
            Card lastCardInfo = lastCardDrawnInfo; // Store the last card's info before updating

            // Start the card deal animation coroutine
            StartCoroutine(DealCardAnimation(cardObject, offScreenPos, initialRot, cardSpawnLocator.transform.position, Quaternion.identity, 1f, drawnCard, lastCardInfo));

            // Update the references to the last card drawn
            lastCardDrawn = cardObject;
            lastCardDrawnInfo = drawnCard;
        }
    }

    private IEnumerator DealCardAnimation(GameObject cardObject, Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot, float duration, Card drawnCard, Card lastCardInfo)
    {
        float time = 0;
        while (time < duration)
        {
            Debug.Log("Card is animating");
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            cardObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            cardObject.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            time += Time.deltaTime;
            yield return null;
        }
        cardObject.transform.position = endPos;
        cardObject.transform.rotation = endRot;

        // Animation is complete, now check the guess
        if (!firstDraw)
        {
            // Check the guess using the last card's info
            CheckGuess(drawnCard, lastCardInfo, lastBet);
        }
        else
        {
            firstDraw = false; // The first card has been drawn and no guess to check
        }
        isAnimating = false;
    }

    private void CheckGuess(Card newCard, Card lastCard, bool isHigher)
    {
        // Compare the new card against the last card
        if ((isHigher && (newCard.rank > lastCard.rank)) || (!isHigher && (newCard.rank < lastCard.rank)))
        {
            currentStreak++;
            Score += currentStreak;
            UIConRef.UpdateScore(Score);
            Debug.Log("Correct guess! New score: " + Score);
            audioSource.PlayOneShot(goodSound);
            if (currentStreak > currentLongestStreak)
            {
                currentLongestStreak = currentStreak;

            }
            if (currentStreak > 1)
            {
                UIConRef.streakText.text = "STREAK X" + currentStreak.ToString();
                UIConRef.streakObj.SetActive(true);
            }
  

        }
        else
        {
            currentStreak = 0;

            UIConRef.streakObj.SetActive(false);
            Lives--;
            UIConRef.UpdateHealth();
            Debug.Log("Incorrect guess! New Lives: " + Lives);
            audioSource.PlayOneShot(badSound);
        }
    }




}
