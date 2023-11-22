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
            Vector3 offScreenPos = new Vector3(-10, cardSpawnLocator.transform.position.y, cardSpawnLocator.transform.position.z);
            Quaternion initialRot = Quaternion.Euler(0, 0, 90);
            GameObject cardObject = Instantiate(cardPrefab, offScreenPos, initialRot, cardSpawnLocator.transform);
            CardController cardController = cardObject.GetComponent<CardController>();
            cardController.SetUpNewCard(drawnCard);
            lastCardDrawn = cardObject;
            lastCardDrawnInfo = drawnCard;
            StartCoroutine(DealCardAnimation(cardObject, offScreenPos, initialRot, cardSpawnLocator.transform.position, Quaternion.identity, 1f));
        }
    }

    private void CheckGuess(Card newCard, bool isHigher)
    {
        if (isHigher && (newCard.rank > lastCardDrawnInfo.rank) || (!isHigher && newCard.rank < lastCardDrawnInfo.rank))
        {
            Score++;
            UIConRef.UpdateScore(Score);
            Debug.Log("Correct guess! New score " + Score);
        }
        else
        {
            Lives--;
            UIConRef.UpdateHealth();
            Debug.Log("Incorrect guess! New Lives " + Lives);
        }
    }

    private IEnumerator DealCardAnimation(GameObject cardObject, Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            Debug.Log("card is animating");
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            cardObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            cardObject.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            time += Time.deltaTime;
            yield return null;
        }
        cardObject.transform.position = endPos;
        cardObject.transform.rotation = endRot;
    }


}
