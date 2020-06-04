using ElevatorSettings;
using UnityEngine;

namespace Environment{
    public class ElevatorPhotocell : MonoBehaviour{
        private ElevatorMovementManager _elevatorMovementManager;
        private ElevatorParameters _elevatorParameters;
        
        public void SetElevatorScripts(ElevatorMovementManager elevatorMovementManager,
                                       ElevatorParameters elevatorParameters){
            _elevatorMovementManager = elevatorMovementManager;
            _elevatorParameters = elevatorParameters;
        }

        private void OnTriggerStay(Collider other){
            if (CheckIfElevatorStateAllowsOpeningClosingDoors()){
                OpenClosingDoors();
            }
        }

        private bool CheckIfElevatorStateAllowsOpeningClosingDoors(){
            return _elevatorParameters.DoorsClosing;
        }

        private void OpenClosingDoors(){
            _elevatorMovementManager.OpenClosingDoors();
        }
    }
}
