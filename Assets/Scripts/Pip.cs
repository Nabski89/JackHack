using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pip : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer m_SpriteRenderer;
    public bool face = false;
    public bool jack = false;
    public bool queen = false;
    public bool king = false;

    void Start()
    {
        //get the number of pips
        Card ParentCard = GetComponentInParent<Card>();

        if (face == false)
        {
            if (ParentCard.Value > 1)
            {
                ParentCard.Value -= 1;
                transform.GetChild(0).gameObject.SetActive(true);
            }


            // change the suit
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            Suit ParentSuitSprite = GetComponentInParent<Suit>();
            m_SpriteRenderer.sprite = ParentSuitSprite.SuitSprite[ParentCard.SuitNum - 1];
        }

        if (face == true)
        {

            if (jack == true && ParentCard.Value != 11)
            {
                gameObject.SetActive(false);
            }
            if (queen == true && ParentCard.Value != 12)
            {
                gameObject.SetActive(false);
            }
            if (king == true && ParentCard.Value != 13)
            {
                gameObject.SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
