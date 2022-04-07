using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    private AudioPlayer audioPlayer;
    private LevelManager levelManager;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int Health
    {
        get { return health; }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0) { audioPlayer.PlayPlayerDamagedClip(); }
    }

    public void Die(Animator animator)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95)
        {
            levelManager.ReloadLevel();
            Destroy(gameObject);
        }
    }
}
