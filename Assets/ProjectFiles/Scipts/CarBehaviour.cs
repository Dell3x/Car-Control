using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarBehaviour : MonoBehaviour
{
    public static CarBehaviour instance;

    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private CarCollision carCollision;

    [SerializeField] private Camera followCamera;
    [SerializeField] private Vector3 cameraPosition;

    [SerializeField] private Animator carAnim;


    [SerializeField] private float forwardCarSpeed;

    [SerializeField] private List<ParticleSystem> smokeparticle;
    
    private Rigidbody carRigidbody;

    private float currentXPosition;
    private float newXPosition;


    private bool isRiding;


    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        playerCollision.OnTurnLeft += TurnLeft;
        playerCollision.OnTurnRight += TurnRight;
        carCollision.OnCarCrashed += CarCrash;
        carCollision.OnCarFinished += CarFinish;
    }


    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        currentXPosition = transform.position.x;
    }

    private void Update()
    {
        if (isRiding)
        {
            followCamera.gameObject.transform.position =
                new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z - 10f);
            Riding();
        }
    }

    private void OnDisable()
    {
        playerCollision.OnTurnLeft -= TurnLeft;
        playerCollision.OnTurnRight -= TurnRight;
        CarCollision.instance.OnCarCrashed -= CarCrash;
        carCollision.OnCarFinished -= CarFinish;

    }

    private void Riding()
    {
        foreach (var smoke in smokeparticle)
        {
            smoke.Play();
        }
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + forwardCarSpeed * Time.deltaTime);

        transform.position = newPos;
    }

    public void TurnLeft()
    {
        newXPosition = currentXPosition - 3f;
        currentXPosition = newXPosition;
        transform.DOMoveX(newXPosition, 1f, false);
    }

    public void TurnRight()
    {
        newXPosition = currentXPosition + 3f;
        currentXPosition = newXPosition;
        transform.DOMoveX(newXPosition, 1f, false);
    }

    private void CarCrash()
    {
        foreach (var smoke in smokeparticle)
        {
            smoke.Stop();
        }
        carRigidbody.useGravity = true;
        carRigidbody.AddForce(0, 10, 20, ForceMode.Impulse);
        isRiding = false;
    }

    private void CarFinish()
    {
        carRigidbody.isKinematic = true;
        isRiding = false;
        var seq = DOTween.Sequence();
        foreach (var smoke in smokeparticle)
        {
            smoke.Stop();
        }
        
        carAnim.SetBool("Finish", true);
    }

    public bool IsRiding
    {
        get => isRiding;
        set => isRiding = value;
    }
}