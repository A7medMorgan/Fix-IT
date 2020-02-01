using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public bool die;
    public float damage_amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            GameManager.Instance.Apllay_damage(damage_amount);
        }
    }
}
