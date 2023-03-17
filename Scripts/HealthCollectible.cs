using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller!= null)
        {
            if (controller.health < controller.maxHealth)
            {
                Destroy(gameObject);
                //第二个会报错，因为health 只有get，咩有设置set
                //controller.maxHealth = 15;
                //comtroller.health = 9;

                controller.ChangeHealth(1);

                controller.PlaySound(collectedClip);
            }
        }
    }
}
