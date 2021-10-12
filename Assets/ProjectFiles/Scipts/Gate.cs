using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Animator circleAnimator;
    private void OnTriggerEnter(Collider other)
    {
        circleAnimator.SetBool("IsOpened", true);
        arrow.SetActive(false);
        transform.DOMoveY(transform.position.y - 10f, 3f, false);
    }
}
