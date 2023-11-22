using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab, cardSpawnLocator;
    public Deck deck;
    private GameObject lastCardDrawn;
    private Card lastCardDrawnInfo;
    public int Score, Lives;
    private bool lastBet, firstDraw = true;
    private UIController UIConRef = null;
    public AudioClip cardSlide, goodSound, badSound, gameOverSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
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
        firstDraw = false;
        Debug.Log("Higher Pressed");
        lastBet = true;
        DealHand();
    }

    public void LowerButtonPress()
    {
        firstDraw = false;
        Debug.Log("Lower Pressed");
        lastBet = false;
        DealHand();
    }

    public void DealHand()
    {
        audioSource.PlayOneShot(cardSlide);
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
    }

    private void CheckGuess(Card newCard, Card lastCard, bool isHigher)
    {
        // Compare the new card against the last card
        if ((isHigher && (newCard.rank > lastCard.rank)) || (!isHigher && (newCard.rank < lastCard.rank)))
        {
            Score++;
            UIConRef.UpdateScore(Score);
            Debug.Log("Correct guess! New score: " + Score);
            audioSource.PlayOneShot(goodSound);
        }
        else
        {
            Lives--;
            UIConRef.UpdateHealth();
            Debug.Log("Incorrect guess! New Lives: " + Lives);
            audioSource.PlayOneShot(badSound);
        }
    }




}
