using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SingularBehaviour(false,false,false)]
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CarCollision carCollision;
    [SerializeField] private GameObject finishLine;

    [SerializeField] private List<ParticleSystem> finishParticles;

    private void OnEnable()
    {
        carCollision.OnCarCrashed += Lose;
        carCollision.OnCarFinished += Win;
    }

    private void OnDisable()
    {
        carCollision.OnCarCrashed -= Lose;
        carCollision.OnCarFinished -= Win;
    }

    private void Lose()
    {
        StartCoroutine(ILoseGame());
    }

    private void Win()
    {
        StartCoroutine(IWinGame());
    }


    private IEnumerator ILoseGame()
    {
        CarBehaviour.instance.IsRiding = false;
        CharacterBehaviour.instance.IsMoving = false;
        yield return new WaitForSeconds(1f);
        UIHandler.instance.ShowLoseWindow();
    }

    private IEnumerator IWinGame()
    {
        foreach (var particle in finishParticles)
        {
            particle.Play();
        }
        CarBehaviour.instance.IsRiding = false;
        CharacterBehaviour.instance.IsMoving = false;
        yield return new WaitForSeconds(1f);
        UIHandler.instance.ShowWinWindow();
    }

    public GameObject FinishLine
    {
        get => finishLine;
        set => finishLine = value;
    }
}