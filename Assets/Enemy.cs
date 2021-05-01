using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttack = 0f;

    public Animator animator;
    public float deathTime;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        getDeathTime();
    }

    void Update()
    {
        if(Time.time >= nextAttack){
            Attack();
            nextAttack = Time.time + 1f / attackRate;
        }
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

    public void TakeDamage(int damage){
        currentHealth -= damage;
        animator.SetTrigger("hit");
        if(currentHealth <= 0){
            Die();
        }
    }

    void Attack(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D player in hitPlayer){
            Debug.Log("teste");
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
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
