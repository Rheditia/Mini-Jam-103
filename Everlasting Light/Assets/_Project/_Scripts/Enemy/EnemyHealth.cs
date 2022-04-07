using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { Die(); }
        if (health > 0) { audioPlayer.PlayEnemyDamagedClip(); }
    }

    public void Die()
    {
        audioPlayer.PlayEnemyDieClip();
        Destroy(gameObject);
    }
}
