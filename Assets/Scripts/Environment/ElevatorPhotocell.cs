using System;
using ElevatorSettings;
using UnityEngine;

namespace Environment{
    public class ElevatorPhotocell : MonoBehaviour{
        [SerializeField] private ElevatorMovementManager elevatorMovementManager;

        private void OnTriggerStay(Collider other){
            if (elevatorMovementManager.DoorsClosing){
                elevatorMovementManager.OpenClosingDoors();
            }
            
        }
    }
}
