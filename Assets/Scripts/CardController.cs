using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{

    public SpriteRenderer suiteTL, suiteTR;
    public TextMeshProUGUI rank;
    public Color red, black;
    public Sprite spade, heart, club, diamond;


    public void SetUpNewCard(Card thisCard)
    {
        Debug.Log("Setting up a new card " + thisCard.suit + " " + thisCard.rank);
        rank.text = thisCard.rank.ToString();
        Color textColor;
        textColor = (thisCard.suit == Card.Suit.Hearts || thisCard.suit == Card.Suit.Diamonds) ? red : black;
        rank.color = textColor;
        if (thisCard.suit == Card.Suit.Clubs)
        {
            suiteTL.sprite = club;
        }
        if (thisCard.suit == Card.Suit.Diamonds)
        {
            suiteTL.sprite = diamond;
        }
        if (thisCard.suit == Card.Suit.Hearts)
        {
            suiteTL.sprite = heart;
        }
        if (thisCard.suit == Card.Suit.Spades)
        {
            suiteTL.sprite = spade;
        }
    }
}
