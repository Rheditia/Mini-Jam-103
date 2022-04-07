using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHolder : MonoBehaviour
{
    [SerializeField] WeaponState startingState;
    [SerializeField] int _charge = 5;
    public int Charge
    {
        get { return _charge; }
        set { _charge = value; }
    }

    WeaponState WeaponInHold;
    public bool isReady { get; private set; }
    
    private Animator animator;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        WeaponInHold = startingState;
        SetInitialState();
    }

    private void Update()
    {
        if (WeaponInHold != WeaponState.Empty) { isReady = WaitForConvertion(); }
    }

    private void SetInitialState()
    {
        if (WeaponInHold == WeaponState.Blue) { animator.SetBool("toBlue", true); }
        else if (WeaponInHold == WeaponState.Red) { animator.SetBool("toRed", true); }
    }

    public void ChangeWeaponMatter(WeaponState weapon)
    {
        audioPlayer.PlayConversionClip();
        WeaponInHold = weapon;
        if(weapon == WeaponState.Blue)
        {
            animator.SetBool("toRed", true);
            WeaponInHold = WeaponState.Red;
        }
        else if(weapon == WeaponState.Red)
        {
            animator.SetBool("toBlue", true);
            WeaponInHold = WeaponState.Blue;
        }
    }

    private bool WaitForConvertion()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f) { return false; }
        else { return true; }
    }

    public WeaponState TakeWeapon()
    {
        animator.SetBool("toRed", false);
        animator.SetBool("toBlue", false);
        WeaponState weapon = WeaponInHold;

        WeaponInHold = WeaponState.Empty;
        isReady = false;

        return weapon;
    }
}
