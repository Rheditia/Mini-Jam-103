using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip enemyDamagedClip;
    [SerializeField] [Range(0f, 1f)] float enemyDamagedVolume = 0.5f;
    [SerializeField] AudioClip enemyDieClip;
    [SerializeField] [Range(0f, 1f)] float enemyDieVolume = 0.5f;
    [SerializeField] AudioClip playerDamagedClip;
    [SerializeField] [Range(0f, 1f)] float playerDamagedVolume = 0.5f;
    [SerializeField] AudioClip playerAttackClip;
    [SerializeField] [Range(0f, 1f)] float playerAttackVolume = 0.5f;
    [SerializeField] AudioClip playerDieClip;
    [SerializeField] [Range(0f, 1f)] float playerDieVolume = 0.5f;
    [SerializeField] AudioClip swordClip;
    [SerializeField] [Range(0f, 1f)] float swordVolume = 0.5f;
    [SerializeField] AudioClip conversionClip;
    [SerializeField] [Range(0f, 1f)] float conversionVolume = 0.5f;

    static AudioPlayer Instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetAudio()
    {
        Instance = null;
        Destroy(gameObject);
    }

    public void PlayEnemyDamagedClip()
    {
        PlayClip(enemyDamagedClip, enemyDamagedVolume);
    }

    public void PlayEnemyDieClip()
    {
        PlayClip(enemyDieClip, enemyDieVolume);
    }

    public void PlayPlayerDamagedClip()
    {
        PlayClip(playerDamagedClip, playerDamagedVolume);
    }

    public void PlayPlayerAttackClip()
    {
        PlayClip(playerAttackClip, playerAttackVolume);
    }

    public void PlayPlayerDieClip()
    {
        PlayClip(playerDieClip, playerDieVolume);
    }

    public void PlaySwordClip()
    {
        PlayClip(swordClip, swordVolume);
    }

    public void PlayConversionClip()
    {
        PlayClip(conversionClip, conversionVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
