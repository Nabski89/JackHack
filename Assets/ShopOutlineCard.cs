using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOutlineCard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = transform.localScale;
        if (newScale.x > 1)

        {
            newScale.x -= .05f;
            newScale.y -= .05f;
            transform.localScale = newScale;
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
