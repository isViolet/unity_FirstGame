using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D body;
    //private Animator anim;
    private Collider2D coll;
    public Transform leftPoint;
    public Transform rightPoint;

    public LayerMask ground;

    private float leftx, rightx;

    private bool faceLeft = true;
    public float speed = 3f;
    public float jumpForce = 10f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement(){
        if(faceLeft){
            if (coll.IsTouchingLayers(ground))
            {
                body.velocity = new Vector2(-speed, jumpForce);
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
            }
            
            if(transform.position.x < leftx){
                faceLeft = false;
                transform.localScale = new Vector3(-1,1,1);
            }

        }else{
            if (coll.IsTouchingLayers(ground))
            {
                body.velocity = new Vector2(speed, jumpForce);
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
            }
            
            if(transform.position.x > rightx){
                faceLeft = true;
                transform.localScale = new Vector3(1,1,1);
            }
        }
    }

    void SwitchAnim ()
    {
        if(anim.GetBool("jumping")){
            if(body.velocity.y < 0.1){
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        if(coll.IsTouchingLayers(ground) && anim.GetBool("falling")){
            anim.SetBool("falling",false);
        }
    }

    
}
