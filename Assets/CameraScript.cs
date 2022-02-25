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
        position.x = 20;
        position.y = 2;
        position.z = 0;

        Vector3 newScale = transform.localScale;
        newScale.x = .5f;
        newScale.y = .5f;
        newScale.z = -1;

        AutoPlayer.timer = 64000;

        foreach (var CardToShop in GameObject.FindObjectsOfType<Card>())
        {
            position.x += 1;
            CardToShop.transform.position = position;
            CardToShop.transform.localScale = newScale;
            if (position.x == 20 + 13)
            {
                position.y += 2;
                position.x = 20;
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
