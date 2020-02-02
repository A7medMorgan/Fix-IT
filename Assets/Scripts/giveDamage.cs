using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.Apllay_damage(50f);
    }
}
