using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]private float damage = 20f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "player")
        {
            GameManager.Instance.Apllay_damage(damage);
            Destroy();
        }
        if (collision.tag == "ground") Destroy();
    }
     void Destroy()
    {
        Destroy(gameObject);
    }

}
