using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Animator Animator { get; private set; }

    private bool _activated;
    public bool Activated
    {
        get { return _activated; }
        set { _activated = value; }
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
}
