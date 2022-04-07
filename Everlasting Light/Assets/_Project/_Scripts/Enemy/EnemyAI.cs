using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] int collisionDamage = 25;
    [SerializeField] float spottedRadius = 5f;

    [SerializeField] LayerMask playerLayer;
    private string playerTag = "Player";


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer);
        if (collision.collider.CompareTag(playerTag))
        {
            collision.collider.GetComponent<PlayerHealth>().TakeDamage(collisionDamage);
            Destroy(gameObject);
        }
    }

    public Collider2D SpottedRangeHandle()
    {
        return Physics2D.OverlapCircle(transform.position, spottedRadius, playerLayer);
    }

    public GameObject SpottedTarget()
    {
        Collider2D player = SpottedRangeHandle();
        return player.gameObject;
    }

    public void LookAtTarget(Transform targetPos)
    {

        if (transform.position.x > targetPos.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < targetPos.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spottedRadius);
    }

}
