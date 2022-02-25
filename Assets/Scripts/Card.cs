using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int Value = 0;
    public int ModValue = 0;
    public int Points = 0;
    public int ModCount = 0;
    public int ModTier = 0;
    public int SuitNum = 2;
    public bool Ace = false;

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
    }
}
