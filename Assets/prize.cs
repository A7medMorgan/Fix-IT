using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prize : MonoBehaviour
{
    private int amount_gold = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            LevelManager.Instance.Update_gold(amount_gold);
            Destroy(gameObject);
        }
    }
}
