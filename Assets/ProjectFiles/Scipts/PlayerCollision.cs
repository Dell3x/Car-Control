using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static PlayerCollision instance;
    
    public event Action OnTurnLeft;
    public event Action OnTurnRight;

    private const string triggerLeft = "TriggerLeft";
    private const string triggerRight = "TriggerRight";


    private bool isCrushed;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerLeft) && !isCrushed)
        {
            OnTurnLeft?.Invoke();
        }
        else if (other.CompareTag(triggerRight) && !isCrushed)
        {
            OnTurnRight?.Invoke();
        }
    }
}
