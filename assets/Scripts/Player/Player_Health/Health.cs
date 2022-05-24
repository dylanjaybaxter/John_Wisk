using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } //you can get this variable from other scripts but can only set it in here
    private Animator anim;
    private bool dead;
    private Rigidbody2D body;
    [SerializeField] private AudioClip hurtSFX;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(10, 11, false); //Make sure cat not invincible
        dead = false;
    }

    public void TakeDamage(float _damage)
    {
        
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            GetComponent<AudioSource>().PlayOneShot(hurtSFX);
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead) // Prevents animation looping
            {
                //player dead
                body.velocity = new Vector2(0, body.velocity.y); // Implements movement
                anim.SetTrigger("die");
                if(GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }
                
                if(GetComponent<PlayerAttack>() != null)
                {
                    GetComponent<PlayerAttack>().enabled = false;
                }
                
                dead = true;



                // Add small delay and transition to end screen.
                StoreLevel.nameOfCurrentLevel = SceneManager.GetActiveScene().name;
                Invoke("gotoendscreen", 3); 

            }
            
        }

    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Sets color to red with slight opaqueness
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(0.25f);

        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }


    private void Update()
    {

    }

    public void gotoendscreen()
    {
        SceneManager.LoadScene("End Screen");
    }

    //This means the player fell into the death zone
    public void Fall()
    {
        //For now just take 3 damage
        TakeDamage(3);
    }
}
