using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab, cardSpawnLocator;
    public Deck deck;

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

    public void DealHand()
    {
        deck.ShuffleDeck();
        Card drawnCard = deck.DrawCard();
        if (drawnCard != null)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardSpawnLocator.transform.position, Quaternion.identity, cardSpawnLocator.transform);
            CardController cardController = cardObject.GetComponent<CardController>();
            cardController.SetUpNewCard(drawnCard);
        }
    }

    //Compare Results


}
