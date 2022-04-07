using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Holder")]
    [SerializeField] Vector2 holderBoxSize = Vector2.zero;
    [SerializeField] Vector3 holderBoxOffset = Vector3.zero;

    [Header("Blue Sword")]
    [SerializeField] Vector2 hitBoxSize = Vector2.zero;
    [SerializeField] Vector3 boxOffset = Vector3.zero;
    [SerializeField] int blueDamage = 25;

    [Header("Red Sword")]
    [SerializeField] float hitRadius = 0f;
    [SerializeField] Vector3 circleOffset = Vector3.zero;
    [SerializeField] int redDamage = 50;

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask weaponHolderLayer;
    [SerializeField] LayerMask switchLayer;

    private Light2D playerLight;

    public int Charge { get; private set; }
    public WeaponState SwordState { get; private set; }

    private bool _takeWeapon;
    public bool TakeWeapon
    {
        get { return _takeWeapon; }
        set { _takeWeapon = value; }
    }

    private bool _putWeapon;
    public bool PutWeapon
    {
        get { return _putWeapon; }
        set { _putWeapon = value; }
    }

    private void Awake()
    {
        playerLight = GetComponentInChildren<Light2D>();
    }

    private void Start()
    {
        SwordState = WeaponState.Empty;
    }

    private void Update()
    {
        if (Charge > 0) { playerLight.intensity = Charge / 5f; }
        else { playerLight.intensity = 0.1f; }
    }

    public void Attack()
    {
        Vector3 offset;
        Collider2D[] enemies = new Collider2D[0];
        if (SwordState == WeaponState.Blue)
        {
            //Debug.Log("blue atk");
            offset = new Vector3(boxOffset.x * transform.localScale.x, boxOffset.y * transform.localScale.y, boxOffset.z);
            enemies = Physics2D.OverlapBoxAll(transform.position + offset, hitBoxSize, 0f, enemyLayer);

            Collider2D platformSwitch = Physics2D.OverlapBox(transform.position + offset, hitBoxSize, 0f, switchLayer);
            if (platformSwitch) { platformSwitch.GetComponent<PlatformSwitch>().ActivateSwitch(); }
        }
        else if (SwordState == WeaponState.Red)
        {
            //Debug.Log("red atk");
            offset = new Vector3(circleOffset.x * transform.localScale.x, circleOffset.y * transform.localScale.y, circleOffset.z);
            enemies = Physics2D.OverlapCircleAll(transform.position + offset, hitRadius, enemyLayer);
        }

        if (enemies.Length > 0)
        {
            foreach(Collider2D enemy in enemies)
            {
                //Damage Enemy
                //Debug.Log("Damaged");
                if (SwordState == WeaponState.Blue) 
                { 
                    Charge -= 1;
                    enemy.GetComponent<EnemyHealth>().TakeDamage(blueDamage);
                }
                else if(SwordState == WeaponState.Red) 
                { 
                    Charge += 1;
                    enemy.GetComponent<EnemyHealth>().TakeDamage(redDamage);
                }
            }
        }
        //Debug.Log(enemies.Length);
    }

    public bool AnimationStatusCheck(Animator animator)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95) { return false; }
        else { return true; }
    }

    public Collider2D CheckWeaponHolderInRange()
    {
        Vector3 offset = new Vector3(holderBoxOffset.x * transform.localScale.x, holderBoxOffset.y * transform.localScale.y, holderBoxOffset.z);
        return Physics2D.OverlapBox(transform.position + offset, holderBoxSize, 0f, weaponHolderLayer);
    }

    public bool CheckWeaponHolderReadyStatus()
    {
        if (CheckWeaponHolderInRange())
        {
            return CheckWeaponHolderInRange().GetComponent<SwordHolder>().isReady;
        }
        else { return false; }
    }

    public void WeaponHolderInterraction()
    {
        Collider2D weaponHolder = CheckWeaponHolderInRange();
        
        if (!weaponHolder) { return; }

        SwordHolder swordHolder = weaponHolder.GetComponent<SwordHolder>();
        if ((SwordState == WeaponState.Empty) && _takeWeapon && swordHolder.isReady)
        {
            SwordState = swordHolder.TakeWeapon();
            Charge = swordHolder.Charge;

            playerLight.intensity = Charge / 5f;
        }
        else if (SwordState != WeaponState.Empty && _putWeapon)
        {
            swordHolder.ChangeWeaponMatter(SwordState);
            swordHolder.Charge = Charge;
            Charge = 0;
            SwordState = WeaponState.Empty;

            playerLight.intensity = 0.1f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + new Vector3(boxOffset.x * transform.localScale.x, boxOffset.y * transform.localScale.y, boxOffset.z), hitBoxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(circleOffset.x * transform.localScale.x, circleOffset.y * transform.localScale.y, circleOffset.z), hitRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(holderBoxOffset.x * transform.localScale.x, holderBoxOffset.y * transform.localScale.y, holderBoxOffset.z), holderBoxSize);
    }
}

public enum WeaponState 
{ 
    Empty, 
    Blue, 
    Red 
}
