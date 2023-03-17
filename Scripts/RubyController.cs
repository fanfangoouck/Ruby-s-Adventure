using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    float speed = 3.0f;

    public int maxHealth = 10;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    float horizontal;
    float vertical;

    Rigidbody2D rigidbody2d;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    //添加音频
    AudioSource audioSource;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        //任意有一个方向动了
        if(!Mathf.Approximately(move.x, 0.0f)||
            !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //lookDirection = move;
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        //用于在idle和move的状态间切换，和实际速度无关哈
        animator.SetFloat("Speed", move.magnitude);



        //计时器
        if (isInvincible)
        {
            //每一帧减去每一帧所用的秒，那一秒就减近似一秒的时间
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.C))
        // if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("c");
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("input k");

            RaycastHit2D hit = Physics2D.Raycast
                (rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            Debug.Log("hit is" + hit);
            bool re = (hit.collider != null);
            Debug.Log("hit.collider is" + re);

            if (hit.collider != null)
            {
                Debug.Log("hit.collider != null");
                NonPlayerCharacter character=
                    hit.collider.gameObject.GetComponent<NonPlayerCharacter>();

                //避免是其他碰撞物体
                if(character != null)
                {
                    Debug.Log("DisplayDialog");
                    character.DisplayDialog();
                }

                Debug.Log("collider  " + hit.collider);

            }
        }


        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;
        //rigidbody2d.MovePosition(position);
    }

   public  void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            animator.SetTrigger("Hit");
            //这里return不返回值，是停止往下运行
            if (isInvincible)
            {
                return;
            }

            //如果现在不是无敌状态，把无敌状态赋予，并赋予这个值2s
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        Debug.Log("amount" + amount);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
