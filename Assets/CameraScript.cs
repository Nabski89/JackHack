using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //    public GameObject PlayerDeck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CameraToShop()
    {
        //move the camera to the shop location
        Vector3 position = transform.position;
        position.x = 30;
        position.y = 0;
        transform.position = position;

        //reuse that vector3, but it's time to lay out the cards in the shop
        position.x = 20.6f;
        position.y = 1;
        position.z = 0;

        Vector3 newScale = transform.localScale;
        newScale.x = .6f;
        newScale.y = .6f;
        newScale.z = -1;

        AutoPlayer.timer = 64000;

        foreach (var CardToShop in GameObject.FindObjectsOfType<Card>())
        {
            if (CardToShop.PlayerCard == true)
            {
                position.x += 1.33f;
                CardToShop.transform.position = position;
                CardToShop.transform.localScale = newScale;
                                    Debug.Log(position.x);
                if (position.x > 20.5f + 17.29f)
                {
                    Debug.Log("Second Row");
                    position.y += 2.3f;
                    position.x = 20.6f;
                }
            }
        }
    }

    public void CameraToGame()
    {
        Vector3 position = transform.position;
        position.x = 0;
        position.y = 0;
        transform.position = position;
    }
}
