using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    public float speed = 10f;
    public float jumpForce = 500f;
    public LayerMask ground;
    public Animator animator;
    public Collider2D coll;
    public Collider2D disColl;

    public Text tx_cherry;
    public Text tx_gem;

    bool jump=false;
    private bool isHurt = false;

    public int cherry;
    public int gem;

    public Transform CellingCheck;

    public AudioSource jumpAudio;
    public AudioSource hurtAudio;
    public AudioSource cherryAudio;
    public AudioSource gemAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        if(Input.GetButtonDown("Jump")　&&　coll.IsTouchingLayers(ground)){
            jump = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        
        SwitchAnim();
    }

    void Movement()
    {
        float hs;
        float hsraw;
        hs = Input.GetAxis("Horizontal");
        hsraw = Input.GetAxisRaw("Horizontal");
        if (hs != 0)
        {
            rigidBody.velocity = new Vector2(hs * speed * Time.deltaTime, rigidBody.velocity.y);
            animator.SetFloat("running",Mathf.Abs(hsraw));
        }
        if (hsraw != 0)
        {
            transform.localScale = new Vector3(hsraw, 1, 1);
        }
        // if(Input.GetButtonDown("Jump")　&&　coll.IsTouchingLayers(ground)){
        //     rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.deltaTime);
        //     animator.SetBool("jumping",true);
        // }
        
        if(jump){
            jump=false;
            jumpAudio.Play();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.deltaTime);
            animator.SetBool("jumping",true);
        }
        Crouch();
        
    }

    void SwitchAnim(){
        animator.SetBool("idle",false);
        if(rigidBody.velocity.y < 0.1f && !coll.IsTouchingLayers(ground)){
            animator.SetBool("falling",true);
        }
        if(animator.GetBool("jumping") ){
            if(rigidBody.velocity.y < 0){
                animator.SetBool("jumping",false);
                animator.SetBool("falling",true);
            }
        }else if (isHurt)
        {
            hurtAudio.Play();
            animator.SetBool("hurt",true);
            animator.SetFloat("running",0.0f);
            if (Mathf.Abs(rigidBody.velocity.x) < 0.1f)
            {
                animator.SetBool("hurt",false);
                animator.SetBool("idle",true);
                isHurt = false;
            }
        }else if(coll.IsTouchingLayers(ground)){
            animator.SetBool("falling",false);
            animator.SetBool("idle",true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D coll){
        if (coll.tag == "Cherry")
        {
            cherryAudio.Play();
            Destroy(coll.gameObject);
            cherry += 1;
            tx_cherry.text = cherry.ToString();
        }else if (coll.tag == "Gem")
        {
            gemAudio.Play();
            Destroy(coll.gameObject);
            gem += 1;
            tx_gem.text = gem.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.tag == "Enemies")
        {
            Enemy em = coll.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("falling"))
            {
                em.destroy();
                // Destroy(coll.gameObject);
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.deltaTime);
                animator.SetBool("jumping",true);
            }else if (transform.position.x < coll.gameObject.transform.position.x)
            {
                rigidBody.velocity = new Vector2(-10f, rigidBody.velocity.y);
                isHurt = true;
            }else if (transform.position.x > coll.gameObject.transform.position.x)
            {
                rigidBody.velocity = new Vector2(10f, rigidBody.velocity.y);
                isHurt = true;
            }
        }
    }

    void Crouch(){
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))
        {
            if(Input.GetButtonDown("Crouch")){
                disColl.enabled = false;
                animator.SetBool("crouch", true);
            }else if (Input.GetButtonUp("Crouch")){
                animator.SetBool("crouch", false);
                disColl.enabled = true;
            }
        }
    }

}
