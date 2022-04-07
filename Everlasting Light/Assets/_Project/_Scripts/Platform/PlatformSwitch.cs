using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{
    Platform platform;

    private void Awake()
    {
        platform = GetComponentInParent<Platform>();
    }

    public void ActivateSwitch()
    {
        platform.Activated = !platform.Activated;
        platform.Animator.SetBool("activated", platform.Activated);
    }
}
