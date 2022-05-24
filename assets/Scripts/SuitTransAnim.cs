using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitTransAnim : MonoBehaviour
{
    private Animator anim;
    public bool walking;
    private void Awake() //Runs only when loading the script
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(walking){
            anim.SetBool("Walking", true);
        }else{
            anim.SetBool("Walking", false);
        }
    }
}
