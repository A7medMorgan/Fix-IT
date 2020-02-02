using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_player : MonoBehaviour
{
    public AudioClip Walk;
    private AudioSource Audio;
    Animator Anim;
    private Character2D Ch;
    private Rigidbody2D rb;
    [SerializeField]float speed = 7f;
    float _current_speed;
    bool crouch;
    bool ground_check;
    private bool _crouch = false;
    [SerializeField] private float health;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Ch = this.GetComponent<Character2D>();
        Audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _current_speed = Input.GetAxis("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.Space)) { rb.velocity /= 4; Ch.Jumb(); }
        if (Input.GetKey(KeyCode.LeftControl)) { crouch = true; }
        else { crouch = false; }
        walk();
    }
    private void FixedUpdate()
    {
        Ch.Move_Horizontal(_current_speed, crouch);
        play_run();
        Crouch();
    }
    private void play_run()
    {
        if (Mathf.Abs(_current_speed) > 0.1f)
            Anim.SetFloat("Run", Mathf.Abs(_current_speed));
        else
            Anim.SetFloat("Run", Mathf.Abs(_current_speed));
    }
    private void Crouch()
    {
        if (crouch && ground_check)
        {
            Anim.SetBool("Crouch", true);
        }
        else { Anim.SetBool("Crouch", false); }
    }
    public void On_ground()
    {
        ground_check = true;
    }
    public void walk()
    {
        if (Mathf.Abs(_current_speed) > 0 && Mathf.Abs(_current_speed) < 5)
        {
            Audio.clip = Walk;
            Audio.Play();
        }
        else {
            Audio.Stop();
        }
    }
}
