using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
   public static CarCollision instance;
   
   public event Action OnCarCrashed;
   public event Action OnCarFinished;

   private const string obstacle = "Obstacle";
   private const string finishTrigger = "FinishTrigger";

   private bool isCrashed;
   

   private void Awake()
   {
      instance = this;
   }

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag(obstacle))
      {
         OnCarCrashed?.Invoke();
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(obstacle) && !isCrashed)
      {
         isCrashed = true;
         OnCarCrashed?.Invoke();
      }
      else if (other.CompareTag(finishTrigger))
      {
         OnCarFinished?.Invoke();
      }
      
   }
}
