using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData", menuName = "Data/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Move Stat")]
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float maxHorizontalVelocity = 3f;
    [SerializeField] float hStopDamping = 0.5f;
    [SerializeField] float hTurnDamping = 0.5f;


    public float Acceleration
    {
        get { return acceleration; }
    }

    public float MaxHorizontalVelocity
    {
        get { return maxHorizontalVelocity; }
    }

    public float HStopDamping
    {
        get { return hStopDamping; }
    }

    public float HTurnDamping
    {
        get { return hTurnDamping; }
    }
}
