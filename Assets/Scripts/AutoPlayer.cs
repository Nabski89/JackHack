using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayer : MonoBehaviour
{
    // Start is called before the first frame update


    public Field Computer;
    public Field Player;

    public static int timer = 30;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        Player.NewHand();
        Computer.NewHand();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1;
        if (timer < 0)
        {
            // start a new hand
            if (Computer.Hold == true && Player.Hold == true)
            {
                Debug.Log("New Hand");
                Computer.NewHand();
                Player.NewHand();
                timer += 30;
            }
            //computer plays after the player
            if (Computer.Hold == false && Player.Hold == true && timer < 0)
            {
                Computer.FlipFirstCardCard();
                Debug.Log("Computer Hit");
                Computer.Hit();
                timer += 30;

                if (Computer.Hold == true && Player.Hold == true)
                {
                    timer += 30 * 4;
                }
            }
            //player goes
            if (Player.Hold == false && timer < 0)
            {
                Debug.Log("Player Hit");
                Player.Hit();
                timer += 30;

                if (Player.Hold == true)
                {
                    Computer.FlipFirstCardCard();
                }

                if (Computer.Hold == true && Player.Hold == true)
                {
                    timer += 30 * 4;
                }
            }
        }
    }
}
