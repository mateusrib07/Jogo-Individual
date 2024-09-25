using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float moveH;
    public float moveV;

    public int speed;
    public float impulse;
    public bool isJumping = false;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool morto = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        transform.position += new Vector3(moveH * speed * Time.deltaTime, 0,0);

        if(Input.GetKey(KeyCode.D))
        {
            anim.SetLayerWeight(2,1);
            sprite.flipX = false;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            anim.SetLayerWeight(2,1);
            sprite.flipX = true;
            
        }
        else
        {
            anim.SetLayerWeight(2,0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(transform.up * impulse, ForceMode2D.Impulse);
            isJumping = true;
            anim.SetLayerWeight(3,1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("chao"))
        {
            isJumping = false;
            anim.SetLayerWeight(3,0);
        }
        if(other.gameObject.CompareTag("limbo"))
        {
            morto = true;
            Morte();
        }
        if(other.gameObject.CompareTag("trofeu"))
        {
            SceneManager.LoadScene("vitoria");
        }

    }

    void Morte()
    {
            SceneManager.LoadScene("derrota");
    }
  
}
