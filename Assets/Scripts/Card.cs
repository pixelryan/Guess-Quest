using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Suit suit;
    public Rank rank;
    public enum Suit
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }

    public enum Rank
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public Card(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
    }
}
