using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public bool Computer = false;
    public GameObject Deck;
    public Field Opponent;
    public GameObject Bust;
    public int handsize = 0;
    public int Score = 0;
    public bool Hold = false;
    public int AceCount = 0;

    public int BustValue = 21;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        if (Hold == false)
        {

            handsize += 1;
            //get a random card from the deck
            var CardDeck = Deck.GetComponentsInChildren<Card>();
            var CardDealt = CardDeck[Random.Range(0, CardDeck.Length)];
            //boy howdy I should add a check to make sure I have cards in the deck or it will crash

            //make this the parent
            CardDealt.transform.SetParent(this.transform);

            //set the card position
            Vector3 position = CardDealt.transform.position;
            position.x = ((2.7f * handsize) - 7.39f);
//            CardDealt.transform.position = position;
            CardDealt.TarX = ((2.7f * handsize) - 7.39f);
            CardDealt.TarY = transform.position.y;
            CardDealt.NeedMove = true;


            //flip the card
            Vector3 newScale = CardDealt.transform.localScale;
            newScale.z = -1;
            CardDealt.transform.localScale = newScale;

            //point check
            CardDealt.PlayCard();
            Score += CardDealt.Points;
            Debug.Log("We are at " + Score);


            //unflip the first card it if the computer
            if (Computer == true && handsize == 1)
            {
                newScale.z = 1;
                CardDealt.transform.localScale = newScale;
            }
            //check if hold
            if (Score >= 17)
            {
                Hold = true;

                if (Computer == true && Score <= BustValue)
                {
                    if (Opponent.Score > Score)
                    {
                        MoneyManager.MoneyAmount += MoneyManager.BetWin;
                    }
                    else
                    {
                        MoneyManager.MoneyAmount -= MoneyManager.BetLose;
                    }
                    BustValue = 21;
                }
            }

            if (Score > BustValue && AceCount > 0)
            {
                AceCount -= 1;
                Score -= 10;
                Debug.Log("Ace Used");
            }
            // check if bust
            if (Score > BustValue)
            {
                Debug.Log("BUST");
                Instantiate(Bust, this.transform);

                //did the player bust? Lose money
                if (Computer == false)
                {
                    MoneyManager.MoneyAmount -= MoneyManager.BetLose;
                    Opponent.Hold = true;
                    BustValue = 21;
                }
                //did the computer bust, win money
                if (Computer == true)
                {
                    MoneyManager.MoneyAmount += MoneyManager.BetWin;
                    BustValue = 21;
                }

            }

        }
    }

    public void NewHand()
    {
        AceCount = 0;
        Hold = false;
        Score = 0;
        //move cards to deck
        while (handsize > 0)
        {
            var CardDeck = GetComponentsInChildren<Card>();
            var CardDealt = CardDeck[Random.Range(0, CardDeck.Length)];

            //ACTUALLY move cards to deck
            Vector3 position = CardDealt.transform.position;
            Vector3 positionDeck = Deck.transform.position;

            CardDealt.transform.position = positionDeck;

            Vector3 newScale = CardDealt.transform.localScale;
            newScale.z = 1;
            CardDealt.transform.localScale = newScale;
            //change parent
            CardDealt.transform.SetParent(Deck.transform);

            handsize -= 1;
        }
        Hit();
        Hit();
    }

    public void FlipFirstCardCard()
    {
        var CardThatNeedsToReFlip = this.gameObject.transform.GetChild(0);
        Vector3 CardThatNeedsToReFlipScale = CardThatNeedsToReFlip.transform.localScale;
        CardThatNeedsToReFlipScale.z = -1;
        CardThatNeedsToReFlip.transform.localScale = CardThatNeedsToReFlipScale;
    }
}
