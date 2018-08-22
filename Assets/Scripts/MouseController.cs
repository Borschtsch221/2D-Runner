using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {


    public float jetPackForce = 30f;
    public float forwardMovementSpeed = 10f;
   // public int coins = 0;
    private bool dead = false;

    public Transform groundCheckTransform;
    private Animator animator;
    private bool grounded;
    public LayerMask groundCheckLayerMask;
    public ParticleSystem jetpack;


    private bool wasDead =false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void AdjustJetpack(bool jetpackActive)
    {
        ParticleSystem.EmissionModule jpEmission = jetpack.emission;
        jpEmission.enabled = !grounded;
        jpEmission.rateOverTime = jetpackActive ? 300.0f : 75.0f;
    }

    void UpdateGroundedStatus()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.3f, groundCheckLayerMask);
        animator.SetBool("grounded", grounded);
    }


    void FixedUpdate()
    {
        bool jetPackActive = Input.GetButton("Fire1");
        if (jetPackActive && !dead)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetPackForce));        
        }
        
        if (dead && !wasDead)
        {
            //forwardMovementSpeed -= Time.fixedDeltaTime * 2;
            Vector2 newVelocity =  GetComponent<Rigidbody2D>().velocity;
            newVelocity.y = Random.Range(3,7);
            newVelocity.x = Random.Range(0, 10);
            wasDead = true;
            GetComponent<Rigidbody2D>().velocity = newVelocity;
        }
        if (!dead)
        {
            Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
            newVelocity.x = forwardMovementSpeed;
            GetComponent<Rigidbody2D>().velocity = newVelocity;
            UpdateGroundedStatus();
            AdjustJetpack(jetPackActive);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LaserScript>() != null)
        {
            Death();
        }
        else
        {
            ScoreScript.coins++;

            Destroy(other.gameObject);
        }
        //other.gameObject.tag

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "palka")
        {
            Debug.Log("palka");
            Death();
        }
    }

    void Death()
    {
        dead = true;
        animator.SetBool("isDead", true);
        jetpack.enableEmission = false;
        ScoreScript.coins = 0;
        StartCoroutine(restart());
        
        
    }

    IEnumerator restart()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("restart");
    }


}
