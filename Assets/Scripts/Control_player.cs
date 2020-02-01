using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_player : MonoBehaviour
{
    Animator animation;
    private Character2D Ch;
    private Rigidbody2D rb;
    [SerializeField]float speed = 15f;
    float num;
    bool crouch;
    bool ground_check;
    private bool _crouch = false;
    [SerializeField] private float health;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Ch = this.GetComponent<Character2D>();
    }

    // Update is called once per frame
    void Update()
    {
        num = Input.GetAxis("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.Space)) { rb.velocity /= 4; Ch.Jumb(); }
        if (Input.GetKey(KeyCode.LeftControl)) { crouch = true; }
        else { crouch = false; }
    }
    private void FixedUpdate()
    {
        Ch.Move_Horizontal(num, crouch);
        play_run();
        Crouch();
    }
    private void play_run()
    {
        if (Mathf.Abs(num) > 0.1f)
            animation.SetFloat("Run", Mathf.Abs(num));
        else
            animation.SetFloat("Run", Mathf.Abs(num));
    }
    private void Crouch()
    {
        if (crouch && ground_check)
        {
            animation.SetBool("Crouch", true);
        }
        else { animation.SetBool("Crouch", false); }
    }
    public void On_ground()
    {
        ground_check = true;
    }
}
