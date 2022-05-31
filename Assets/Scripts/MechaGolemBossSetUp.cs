using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaGolemBossSetUp : MonoBehaviour
{
    public AudioSource BossAwakeSfx;
    public GameObject mechaGolem;
    private GameObject awakeningTrigger;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        awakeningTrigger = GameObject.Find("Awakening Trigger");
        animator = mechaGolem.GetComponent<Animator>();

        mechaGolem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        mechaGolem.GetComponent<CapsuleCollider2D>().enabled = false;
        mechaGolem.GetComponent<HealthSystem>().enabled = false;
        mechaGolem.GetComponent<ContactDamage>().enabled = false;
        mechaGolem.GetComponent<MoveTowardsPlayer>().enabled = false;
        mechaGolem.GetComponent<ShooterEnemy>().enabled = false;
        mechaGolem.GetComponent<HealthBar>().enabled = false;
        mechaGolem.GetComponent<EnemyView>().enabled = false;
        BossAwakeSfx = GameObject.Find("BossAwakeSfx").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("FirstTouchedByPlayer", true);

            BossAwakeSfx.Play();
            StartCoroutine(Wait());
            awakeningTrigger.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);

        mechaGolem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        mechaGolem.GetComponent<CapsuleCollider2D>().enabled = true;
        mechaGolem.GetComponent<HealthSystem>().enabled = true;
        mechaGolem.GetComponent<ContactDamage>().enabled = true;
        mechaGolem.GetComponent<MoveTowardsPlayer>().enabled = true;
        mechaGolem.GetComponent<ShooterEnemy>().enabled = true;
        mechaGolem.GetComponent<HealthBar>().enabled = true;
        //mechaGolem.GetComponent<EnemyView>().enabled = true;
        awakeningTrigger.GetComponent<BoxCollider2D>().enabled = false;
        
        mechaGolem.GetComponent<GolemAI>().enabled = true;
    }
}
