using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    float speed = 3.0f;

    public  float changeTime = 2.0f;
    float timer;
  

    public bool vertical;
    int direction = -1;
    public Animator animator;

    bool broken = false;

    public ParticleSystem smokeEffect;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        timer -= Time.deltaTime;

        if (broken)
        {
            return;
        }


        if (timer < 0)
        {
            direction = -direction;
            animator.SetFloat("MoveX", direction);
            timer = changeTime;
        }

    }



    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 position = transform.position;

        if (broken)
        {
            return;
        }

        if (vertical)
        {
            position.y = position.y + speed * Time.deltaTime*direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + speed * Time.deltaTime * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
      
        rigidbody2d.MovePosition(position);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController ruby = other.gameObject.GetComponent<RubyController>();

        if(ruby != null)
        {
            ruby.ChangeHealth(-1);
        }

    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        smokeEffect.Stop();
    }
}
