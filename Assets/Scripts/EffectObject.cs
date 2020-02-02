using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public GameObject ToDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Destroyit();
        }
    }
    public void Destroyit()
    {
        if(ToDestroy!=null)
        Destroy(ToDestroy);
    }
}
