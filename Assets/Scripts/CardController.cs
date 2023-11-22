using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{

    public Image suiteTL, suiteTR;
    public TextMeshProUGUI rank;
    public Color red, black;
    public Sprite spade, heart, club, diamond;


    public void SetUpNewCard(Card thisCard)
    {
        Debug.Log("Setting up a new card " + thisCard.suit + " " + thisCard.rank);
        rank.text = RankToString(thisCard.rank);
        Color textColor;
        textColor = (thisCard.suit == Card.Suit.Hearts || thisCard.suit == Card.Suit.Diamonds) ? red : black;
        rank.color = textColor;
        Sprite suiteSprite = null;
        switch (thisCard.suit)
        {
            case Card.Suit.Clubs:
                suiteSprite = club;
                break;
            case Card.Suit.Diamonds:
                suiteSprite = diamond;
                break;
            case Card.Suit.Hearts:
                suiteSprite = heart;
                break;
            case Card.Suit.Spades:
                suiteSprite = spade;
                break;
        }
        suiteTL.sprite = suiteSprite;
        suiteTR.sprite = suiteSprite;
    }

    private string RankToString(Card.Rank rank)
    {
        switch (rank)
        {
            case Card.Rank.Ace:
                return "A";
            case Card.Rank.Two:
                return "2";
            case Card.Rank.Three:
                return "3";
            case Card.Rank.Four:
                return "4";
            case Card.Rank.Five:
                return "5";
            case Card.Rank.Six:
                return "6";
            case Card.Rank.Seven:
                return "7";
            case Card.Rank.Eight:
                return "8";
            case Card.Rank.Nine:
                return "9";
            case Card.Rank.Ten:
                return "10";
            case Card.Rank.Jack:
                return "J";
            case Card.Rank.Queen:
                return "Q";
            case Card.Rank.King:
                return "K";
            default:
                return rank.ToString();
        }
    }
}
