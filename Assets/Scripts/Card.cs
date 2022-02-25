using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject OUTLINE;
    public bool CompCard = false;
    public bool PlayerCard = false;
    public bool StoreCard = true;
    bool ShopSelected = false;
    public int Value = 0;
    public int ModValue = 0;
    public int Points = 0;
    public int ModCount = 0;
    public int ModTier = 0;
    public int SuitNum = 2;

    public int MoneyModNUM = 0;
    public int MoneyModNUMLose = 0;
    public float MoneyModPERCENT = 1;
    public bool Ace = false;

    public int TimerMod = 0; // 0 to 10

    int PointMod = 0;
    // Start is called before the first frame update
    void Awake()
    {
        ModValue = Mathf.Clamp(Value, 1, 10);

        //check if we have modifiers we can use
        if (ModCount > 0)
        {
            //roll that many mods to the card
            //of x tier
        }
        Points = ModValue + PointMod;
    }
    void Start()
    {

    }

    public void PlayCard()
    {
        Field HandPlayed = gameObject.GetComponentInParent(typeof(Field)) as Field;
        if (Ace == true)
        {
            HandPlayed.AceCount += 1;
            Debug.Log("AcePlayed");
        }
        MoneyManager.BetWin += MoneyModNUM;
        MoneyManager.BetLose += MoneyModNUMLose;
        MoneyManager.BetWin = Mathf.Floor(MoneyManager.BetWin * MoneyModPERCENT);
        AutoPlayer.timer += TimerMod;
    }

    void OnMouseDown()
    {
        //this is the trash we are using to decide if it is in the store or not.
        Vector3 position = transform.position;


        if (PlayerCard == true)
        {
            foreach (var OtherCard in GameObject.FindObjectsOfType<Card>())
            {
                if (OtherCard.PlayerCard == true)
                {
                    ShopSelected = false;
                }

                //This is the worst fucking code solution I've ever used. It's so niche and dirty I hate it, but thinking is hard.
                foreach (var ShopOutline in GameObject.FindObjectsOfType<ShopOutlineCard>())
                {
                    position = ShopOutline.transform.position;
                    if (position.y > 0)
                    {
                        ShopOutline.Die();
                    }
                }
            }
        }
        if (StoreCard == true)
        {
            foreach (var OtherCard in GameObject.FindObjectsOfType<Card>())
            {
                if (OtherCard.StoreCard == true)
                {
                    ShopSelected = false;
                }
                //This is the worst fucking code solution I've ever used. It's so niche and dirty I hate it, but thinking is hard. AND NOW I COPY PASTED IT
                foreach (var ShopOutline in GameObject.FindObjectsOfType<ShopOutlineCard>())
                {
                    position = ShopOutline.transform.position;
                    if (position.y < 0)
                    {
                        ShopOutline.Die();
                    }
                }
            }
        }

        position = transform.position;
        if (position.x > 20)
        {
            Instantiate(OUTLINE, this.transform);
            ShopSelected = true;
        }
    }

    void ShopBuy()
    {
        int DoWeHaveTwoCards = 0;
        foreach (var OtherCard in GameObject.FindObjectsOfType<Card>())
        {
            ShopSelected = true;
            DoWeHaveTwoCards += 1;
        }
        if (DoWeHaveTwoCards == 2)
        {
            foreach (var OtherCard in GameObject.FindObjectsOfType<Card>())
            {
                //get rid of the card in our deck
                if (OtherCard.PlayerCard == true && OtherCard.ShopSelected == true)
                {
                    Destroy(OtherCard);
                }
                //get rid of the shop cards we didn't buy
                if (OtherCard.StoreCard == true && OtherCard.ShopSelected == false)
                {
                    Destroy(OtherCard);
                }
                //add the shop card to our deck
                if (OtherCard.StoreCard == true && OtherCard.ShopSelected == true)
                {
                    OtherCard.ShopSelected = false;
                    OtherCard.StoreCard = false;
                    OtherCard.PlayerCard = true;
                    //then we have to change the parent OH NO
                }
            }


        }
    }
}
