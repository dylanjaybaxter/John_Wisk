using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catAnimSwitch : MonoBehaviour
{
    private Animator anim;
    public bool sleeping = false;
    public bool attack = false;

    public bool walking = false;
    private void Awake() //Runs only when loading the script
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sleeping){
            anim.SetBool("Sleeping", true);
        }else{
            anim.SetBool("Sleeping", false);
        }
        if(attack){
            anim.SetTrigger("attack");
            //GetComponent<AudioSource>().PlayOneShot(hairballSFX);
        }
        if(walking){
            anim.SetBool("walking", true);
            //GetComponent<AudioSource>().PlayOneShot(hairballSFX);
        }
        else{
            anim.SetBool("walking", false);
        }
    }
}
