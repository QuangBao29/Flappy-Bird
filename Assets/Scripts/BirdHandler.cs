using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHandler : MonoBehaviour
{
    private string WALL_TAG = "Wall";
    private string GROUND_TAG = "Ground";

    [SerializeField]
    private float JumpForce = 20;

    private float MaxVelocity = 9;
    private GameObject obj;
    private Rigidbody2D rigid;
    private Animator anim;

    [SerializeField]
    private AudioClip flyClip;
    [SerializeField]
    private AudioClip GameOverClip;
    [SerializeField]
    private AudioClip ScoreClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        rigid = obj.GetComponent<Rigidbody2D>();
        rigid.bodyType = RigidbodyType2D.Kinematic;
        audioSource = gameObject.GetComponent<AudioSource>();
        anim = obj.GetComponent<Animator>();
        anim.SetBool("Fly", false);
        anim.SetBool("Dead", false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
    }
    
    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("Fly", true);
            rigid.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            if (rigid.velocity.y > MaxVelocity)
            {
                float BreakSpeed = rigid.velocity.magnitude - MaxVelocity;
                //Vector2 normalizedVelocity = rigid.velocity.normalized;
                //Vector2 ModifiedVelocity = normalizedVelocity * BreakSpeed;
                
                rigid.AddForce(-new Vector2(0f, BreakSpeed), ForceMode2D.Impulse);
            }
            
            if (!GameHandlers.instance.IsDead)
            {
                audioSource.clip = flyClip;
                audioSource.Play();
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(WALL_TAG) || collision.gameObject.CompareTag(GROUND_TAG))
        {
            anim.SetBool("Dead", true);
            audioSource.clip = GameOverClip;
            audioSource.Play();
            GameHandlers.instance.EndGame(obj);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(WALL_TAG))
        {
            audioSource.clip = ScoreClip;
            audioSource.Play();
            GameHandlers.instance.Scoring();
        }
    }
    

}//class
