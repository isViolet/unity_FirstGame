using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    protected Animator anim;
    protected AudioSource deathAudio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    void Death(){
        Destroy(gameObject);
    }

    public void destroy(){
        
        anim.SetTrigger("death");
        deathAudio.Play();
    }
}
