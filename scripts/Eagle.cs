using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{

    private Rigidbody2D body;
    private Collider2D coll;
    public Transform upPoint;
    public Transform bottomPoint;
    private float up, bottom;
    public float speed = 3f;
    public bool isUp;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        //anim = GetComponent<Animator>();
        transform.DetachChildren();
        up = upPoint.position.y;
        bottom = bottomPoint.position.y;
        Destroy(upPoint.gameObject);
        Destroy(bottomPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement(){
        if (isUp)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            if (transform.position.y > up)
            {
                isUp = false;
            }
        }else{
            body.velocity = new Vector2(body.velocity.x, -speed);
            if (transform.position.y < bottom)
            {
                isUp = true;
            }
        }
    }

}
