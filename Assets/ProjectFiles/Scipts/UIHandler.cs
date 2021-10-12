using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    [SerializeField] private Button startButton;
    [SerializeField] private Slider waySlider;

    [SerializeField] private GameObject loseWindow;
    [SerializeField] private GameObject winWindow;

    private Animator winAndLoseAnimator;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        SetSliderValue();
    }

    private void Update()
    {
        UpdateSliderValue();
    }


    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        CarBehaviour.instance.IsRiding = true;
        CharacterBehaviour.instance.IsMoving = true;
    }

    private void SetSliderValue()
    {
        waySlider.minValue = CharacterBehaviour.instance.transform.position.z;
        waySlider.maxValue = GameManager.Instance.FinishLine.transform.position.z;
    }

    private void UpdateSliderValue()
    {
        waySlider.value = CharacterBehaviour.instance.transform.position.z;
    }

    public void ShowLoseWindow()
    {
        winAndLoseAnimator = loseWindow.GetComponent<Animator>();
        loseWindow.SetActive(true);
        winAndLoseAnimator.SetBool("ShowWindow", true);
    }

    public void ShowWinWindow()
    {
        winAndLoseAnimator = winWindow.GetComponent<Animator>();
        winWindow.SetActive(true);
        winAndLoseAnimator.SetBool("ShowWindow", true);
    }
}