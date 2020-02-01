using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class test1 : MonoBehaviour
{
     Animator ani;
    private Character2D Ch;
    private Rigidbody2D rb;
    float speed = 20f;
    float num;
    bool crouch = false;
    [SerializeField] private float health;
    private void Awake()
    {
        health = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Ch = this.GetComponent<Character2D>();
    }

    // Update is called once per frame
    void Update()
    {
        num = Input.GetAxis("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.Space)) Ch.Jumb();
        if (Input.GetKey(KeyCode.LeftControl)) crouch = true;
        else crouch = false;
    }
    private void FixedUpdate()
    {
        Ch.Move_Horizontal(num, crouch);
        if (Mathf.Abs(num) > 0.1f)
            ani.SetBool("Run", true);
        else
            ani.SetBool("Run", false);
    }
}
