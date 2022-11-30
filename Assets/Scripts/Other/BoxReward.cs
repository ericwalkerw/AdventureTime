using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxReward : MonoBehaviour
{
    public int boxValue = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.AddScore(boxValue);

            Destroy(gameObject);
        }
    }
}
