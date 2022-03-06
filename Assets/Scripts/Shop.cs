using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopCard;
    Vector3 CardSpot1;
    Vector3 CardSpot2;
    Vector3 CardSpot3;
    void Start()
    {
        CardSpot1 = transform.position;
        CardSpot2 = transform.position;
        CardSpot3 = transform.position;

        CardSpot2.x += 2.5f;
        CardSpot3.x += 5f;
    }
    public void SpawnCards()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Instantiate(ShopCard, CardSpot1, Quaternion.identity, this.transform);
        Instantiate(ShopCard, CardSpot2, Quaternion.identity, this.transform);
        Instantiate(ShopCard, CardSpot3, Quaternion.identity, this.transform);
    }
}
