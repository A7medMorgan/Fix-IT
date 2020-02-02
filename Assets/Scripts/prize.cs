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
            GameManager.Instance.collect_gold(amount_gold);
            GameManager.Instance.Increse_health(40);
            Destroy(gameObject);
        }
    }
}
