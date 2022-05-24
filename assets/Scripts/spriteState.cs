using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteState : MonoBehaviour
{
    private Animator anim;
    public bool struggle = false;
    private void Awake() //Runs only when loading the script
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(struggle){
            anim.SetBool("Struggle", true);
        }else{
            anim.SetBool("Struggle", false);
        }
    }
}
