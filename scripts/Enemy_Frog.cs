using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{
    private Rigidbody2D body;
    public Transform leftPoint;
    public Transform rightPoint;

    private float leftx, rightx;

    private bool faceLeft = true;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement(){
        if(faceLeft){
            body.velocity = new Vector2(-speed, body.velocity.y);
            if(transform.position.x < leftx){
                faceLeft = false;
                transform.localScale = new Vector3(-1,1,1);
            }
        }else{
            body.velocity = new Vector2(speed, body.velocity.y);
            if(transform.position.x > rightx){
                faceLeft = true;
                transform.localScale = new Vector3(1,1,1);
            }
        }
    }
}
