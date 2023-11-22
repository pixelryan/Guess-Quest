using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab, cardSpawnLocator;
    public Deck deck;
    private GameObject lastCardDrawn;

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
        Debug.Log("Higher Pressed");
        DealHand();
    }

    public void LowerButtonPress()
    {
        Debug.Log("Lower Pressed");
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
        if (drawnCard != null)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardSpawnLocator.transform.position, Quaternion.identity, cardSpawnLocator.transform);
            CardController cardController = cardObject.GetComponent<CardController>();
            cardController.SetUpNewCard(drawnCard);
            lastCardDrawn = cardObject;
        }
    }

    //Compare Results


}
