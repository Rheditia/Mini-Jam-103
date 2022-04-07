using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Sword Attack")]
    [SerializeField] int hitDamage = 15;
    [SerializeField] Vector2 hitBoxSize = Vector2.zero;
    [SerializeField] Vector3 boxOffset = Vector3.zero;

    [SerializeField] LayerMask playerLayer;

    public void Attack()
    {
        Collider2D player = AttackRangeHandler();
        if (player)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(hitDamage);
        }
    }

    public Collider2D AttackRangeHandler()
    {
        Vector3 offset = new Vector3(boxOffset.x * transform.localScale.x, boxOffset.y * transform.localScale.y, boxOffset.z);
        return Physics2D.OverlapBox(transform.position + offset, hitBoxSize, 0f, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(boxOffset.x * transform.localScale.x, boxOffset.y * transform.localScale.y, boxOffset.z), hitBoxSize);
    }
}
