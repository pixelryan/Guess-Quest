using System;
using System.Collections.Generic;

public class Deck 
{
    private List<Card> cards = new List<Card>();

    public Deck()
    {
        CreateDeck();
    }

    private void CreateDeck()
    {
        foreach (Card.Suit suit in System.Enum.GetValues(typeof(Card.Suit)))
        {
            foreach (Card.Rank rank in System.Enum.GetValues(typeof(Card.Rank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    public Card DrawCard()
    {
        if(cards.Count > 0)
        {
            Card cardToDraw = cards[0];
            cards.RemoveAt(0);
            return cardToDraw;
        }
        else
        {
            return null;
        }
    }

    public void ShuffleDeck()
    {
        //shuffle using the Fisher-Yates method
        System.Random random = new System.Random();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }
}
