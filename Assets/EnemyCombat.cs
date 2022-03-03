using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float startAttackRange = 1f;
    public float hitRange = 1f;
    public Transform player;
    public bool cooldown = false;
    public float cooldownDuration = 1.0f;

    private void Update()
    {
        if (cooldown)
            return;
        else if (Vector2.Distance(player.position, transform.position) <= startAttackRange)
        {
            Attack();
            StartCoroutine(StartCooldown());
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, hitRange, LayerMask.GetMask("Player"));

        var player = hitColliders.Where(c => c.name.Equals("CombatStats")).First();
        player.GetComponent<PlayerCombatStats>().TakeDamage(50);

        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    enemy.GetComponent<PlayerCombatStats>().TakeDamage(50.0f);
        //}
    }

    public IEnumerator StartCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        cooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, hitRange);
    }
}
