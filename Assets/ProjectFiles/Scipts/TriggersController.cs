using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggersController : MonoBehaviour
{
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private List<GameObject> gates;
    
    private void OnEnable()
    {
        playerCollision.OnTurnLeft += MoveGateDown;
        playerCollision.OnTurnRight += MoveGateDown;
    }

    private void OnDisable()
    {
        playerCollision.OnTurnLeft -= MoveGateDown;
        playerCollision.OnTurnRight -= MoveGateDown;

    }

    private void MoveGateDown()
    {
        foreach (var gate in gates)
        {
            gate.transform.DOMoveY(transform.position.y - 10f, 3f, false);
        }
    }
}
