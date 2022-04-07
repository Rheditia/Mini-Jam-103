using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public UnarmedIdleState UnarmedIdleState { get; private set; }
    public UnarmedRunState UnarmedRunState { get; private set; }
    public ArmedIdleState ArmedIdleState { get; private set; }
    public ArmedRunState ArmedRunState { get; private set; }
    public ArmedAttackState ArmedAttackState { get; private set; }
    public PutSwordState PutSwordState { get; private set; }
    public TakeSwordState TakeSwordState { get; private set; }
    public DieState DieState { get; private set; }


    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerWeapon PlayerWeapon { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public Animator Animator { get; private set; }
    public AudioPlayer AudioPlayer { get; private set; }
    public RemnantSpawner RemnantSpawner { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        UnarmedIdleState = new UnarmedIdleState(this, StateMachine, "idle");
        UnarmedRunState = new UnarmedRunState(this, StateMachine, "run");
        ArmedIdleState = new ArmedIdleState(this, StateMachine, "idle");
        ArmedRunState = new ArmedRunState(this, StateMachine, "run");
        ArmedAttackState = new ArmedAttackState(this, StateMachine, "attack");
        PutSwordState = new PutSwordState(this, StateMachine, "put");
        TakeSwordState = new TakeSwordState(this, StateMachine, "take");
        DieState = new DieState(this, StateMachine, "die");

        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerWeapon = GetComponent<PlayerWeapon>();
        PlayerHealth = GetComponent<PlayerHealth>();
        Animator = GetComponent<Animator>();
        AudioPlayer = FindObjectOfType<AudioPlayer>();
        RemnantSpawner = FindObjectOfType<RemnantSpawner>();
    }

    private void Start()
    {
        StateMachine.InitializeState(UnarmedIdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
}
