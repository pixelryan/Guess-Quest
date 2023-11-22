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
    // Start is called before the first frame update
    void Start()
    {
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
        if (lastCardDrawn != null)
        {
            Destroy(lastCardDrawn);
        }
        deck.ShuffleDeck();
        Card drawnCard = deck.DrawCard();
        if (!firstDraw) {
            CheckGuess(drawnCard, lastBet);
        }

        if (drawnCard != null)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardSpawnLocator.transform.position, Quaternion.identity, cardSpawnLocator.transform);
            CardController cardController = cardObject.GetComponent<CardController>();
            cardController.SetUpNewCard(drawnCard);
            lastCardDrawn = cardObject;
            lastCardDrawnInfo = drawnCard;
        }
    }

    private void CheckGuess(Card newCard, bool isHigher)
    {
        if (isHigher && (newCard.rank > lastCardDrawnInfo.rank) || (!isHigher && newCard.rank < lastCardDrawnInfo.rank))
        {
            Score++;
            Debug.Log("Correct guess! New score " + Score);
        }
        else
        {
            Lives--;
            Debug.Log("Incorrect guess! New Lives " + Lives);
        }
    }

    //Compare Results


}
