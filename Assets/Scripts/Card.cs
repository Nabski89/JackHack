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

    //Mods!
    int WinMod = 0;
    int LoseMod = 0;
    int TimerMod = 0;
    int BustMod = 0;
    int BetLimitMod = 0;
    public bool Ace = false;
    int PointMod = 0;
    int CurseMod = 0;
    float BetWinMultMod = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (Value == 0)
        {
            Value = Random.Range(1, 13);
            SuitNum = Random.Range(1, 5);
        }
        ModValue = Mathf.Clamp(Value, 1, 10);
    }
    void Start()
    {
        CardParent Spawner = gameObject.GetComponentInParent(typeof(CardParent)) as CardParent;
        if (Spawner != null)
        {
            ModCount = Spawner.ModCountHold;
            ModTier = Spawner.ModTierHold;
        }
        Points = ModValue + PointMod;
    }

    public void PlayCard()
    {
        Field HandPlayed = gameObject.GetComponentInParent(typeof(Field)) as Field;

        MoneyManager.BetWin += WinMod;
        MoneyManager.BetLose += LoseMod;
        AutoPlayer.timer += TimerMod;
        //mod 4
        HandPlayed.BustValue += BustMod;

        //mod 5
        MoneyManager.BetDefault += BetLimitMod;

        //mod 6
        if (Ace == true)
        {
            HandPlayed.AceCount += 1;
            Debug.Log("AcePlayed");
        }
        //mod 7
        MoneyManager.CursePoints += CurseMod;

        //mod 8
        //automatically applied

        // mod 9
        MoneyManager.BetWin = Mathf.Floor(MoneyManager.BetWin * (1 + BetWinMultMod / 10));

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

    public void RollCard()
    {
        while (ModCount > 0)
        {
            ModCount -= 1;
            var ModNum = Random.Range(4 * (ModTier), 3 + 4 * (ModTier));

            //positive mod 1
            //get extra money on win
            if (ModNum == 0)
            {
                WinMod += Value;
            }
            //positive mod 2
            //lose less money
            if (ModNum == 1)
            {
                LoseMod -= Value;
            }
            //positive mod 3
            //reduce play timer
            if (ModNum == 2)
            {
                TimerMod -= Value;
            }
            //positive mod 4
            //increase bust limit by 1
            if (ModNum == 3)
            {
                BustMod += 1;
            }
            //positive mod 5
            //permanently raise the bet limit by 1
            if (ModNum == 4)
            {
                BetLimitMod = 1;
            }
            //positive mod 6
            //Acts like an ace on bust
            if (ModNum == 5)
            {
                Ace = true;
            }
            //positive mod 7
            //if you bust, you can add a nerf card to the opponent deck
            //not currently doing the bust part!
            if (ModNum == 6)
            {
                CurseMod = Value;
            }
            //positive mod 8
            //card is worth one less
            if (ModNum == 7)
            {
                PointMod = -1;
            }
            //positive mod 9
            //if this is the last card played, double your win
            //not currently enabled!!!!!!!!!!!!!!!!!!!
            if (ModNum == 8)
            {
                BetWinMultMod = Value;
            }


            //negative mod 1
            //reduce bust limit

            //negative mod 2
            //double your loss on loss

            //negative mod 3
            //increase play timer

            //negative mod 4
            //lose $ on play

            //negative mod 5
            //card is worth 1 more     
        }
    }

    public float TarX;
    public float TarY;
    public bool NeedMove = false;
    void Update()
    {
        if (NeedMove == true)
        {
            Vector3 position = transform.position;
            position.z = -0.5f;
            position.x += (TarX - position.x) / 10;
            position.y += (TarY - position.y) / 2;
            transform.position = position;

            if (Mathf.Abs(transform.position.x - TarX) < .1)
            {
                position.x = TarX;
                position.y = TarY;
                position.z = 0;
                transform.position = position;
                NeedMove = false;
            }
        }
    }
}
