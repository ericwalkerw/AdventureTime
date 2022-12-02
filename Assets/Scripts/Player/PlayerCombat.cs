using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer, attackRadius, attackDamage;
    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        CheckCombat();
        checkAttack();
    }
    private void CheckCombat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }
    private void checkAttack()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking =true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("Attack1",true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking); 
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    
    }
    private void CheckAttackingHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attackRadius,whatIsDamageable);

        foreach (Collider2D item in detectedObjects)
        {
            item.transform.parent.SendMessage("Damage", attackDamage);
        }
    } 
    private void FinishAttack()
    {
        isAttacking = false;
        anim.SetBool("Attack1", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attackRadius);
    }

}
