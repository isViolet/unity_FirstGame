using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    public float speed = 10f;
    public float jumpForce = 10f;
    public LayerMask ground;
    private Animator animator;
    private Collider2D coll;

    bool jump=false;

    public int cherry;
    public int gem;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    void Update(){
        if(Input.GetButtonDown("Jump")){
            jump=true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
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
        if(jump){
            jump=false;

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.deltaTime);
            animator.SetBool("jumping",true);
        }
        
    }

    void SwitchAnim(){
        animator.SetBool("idle",false);
        if(animator.GetBool("jumping") ){
            if(rigidBody.velocity.y < 0){
                animator.SetBool("jumping",false);
                animator.SetBool("falling",true);
            }
        }else if(coll.IsTouchingLayers(ground)){
            animator.SetBool("falling",false);
            animator.SetBool("idle",true);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll){
        if (coll.tag == "Cherry")
        {
            Destroy(coll.gameObject);
            cherry += 1;
        }else if (coll.tag == "Gem")
        {
            Destroy(coll.gameObject);
            gem += 1;
        }
    }

}
