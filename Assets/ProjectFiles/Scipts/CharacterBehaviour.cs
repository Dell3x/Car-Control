using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterBehaviour : MonoBehaviour
{
    public static CharacterBehaviour instance;

    [SerializeField] private CarCollision carCollision;

    [SerializeField] private GameObject racer;

    [SerializeField] private float forwardMoveSpeed;
    [SerializeField] private Camera mainCamera;

    private Animator playerAnim;

    private float touchXOffset;

    private bool isMoving;

    private void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        carCollision.OnCarFinished += Finish;
        carCollision.OnCarCrashed += Fail;
    }
    private void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        touchXOffset = 0;

        if (isMoving)
        {
            if (Input.touchCount >= 1)
            {
                touchXOffset = Input.touches[0].deltaPosition.x * 0.03f;
            }

            if (Input.GetMouseButton(0))
            {
                var mousePosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                touchXOffset = mousePosition.x * 0.03f;
            }

            Movement(touchXOffset);
        }
    }
    void OnDisable()
    {
        CarCollision.instance.OnCarFinished -= Finish;
        CarCollision.instance.OnCarCrashed -= Fail;

    }
    private void Movement(float touchMovementValue)
    {
        Vector3 newPos = new Vector3(transform.position.x + touchMovementValue, transform.position.y,
            transform.position.z + forwardMoveSpeed * Time.deltaTime);

        newPos = new Vector3(Mathf.Clamp(newPos.x, -3f, 3f), newPos.y, newPos.z);

        playerAnim.SetBool("Moving", true);
        transform.position = newPos;
    }

    private void Fail()
    {
        playerAnim.SetBool("Moving", false);
        playerAnim.SetBool("Lose", true);
    }

    private void Finish()
    {
        racer.transform.DORotate(new Vector3(0, 180, 0), 2f, RotateMode.FastBeyond360);
        playerAnim.SetBool("Moving", false);
        playerAnim.SetBool("Finish", true);
    }

    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }
}