using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpTrigger : MonoBehaviour
{
    private Animator anim;
    public bool exploding = false;
    private void Awake() //Runs only when loading the script
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(exploding){
            anim.SetBool("Explosion Trigger", true);
        }else{
            anim.SetBool("Explosion Trigger", false);
        }
    }
}
