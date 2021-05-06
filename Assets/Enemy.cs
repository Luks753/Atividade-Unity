using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttack = 0f;

    Rigidbody2D rb;
    public Animator animator;
    public float deathTime;

    public int maxHealth = 100;
    int currentHealth;
    public bool isBoss;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        getDeathTime();
    }

    void Update()
    {
        Attack();
    }

    public void getDeathTime()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name == "death"){
                deathTime = clip.length;
            }
        }
    }

    public void TakeDamage(int damage, Vector2 attack){
        currentHealth -= damage;
        animator.SetTrigger("hit");
        if(currentHealth <= 0){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Die();
        }   
    }

    void Attack(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D player in hitPlayer){
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }

    void Die(){
        animator.SetBool("isDead", true);
        StartCoroutine(WaitDeath());
    }

    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(deathTime);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        gameObject.SetActive(false);
        if(isBoss){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
