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
            if (_elevatorParameters.DoorsClosing){
                _elevatorMovementManager.OpenClosingDoors();
            }
            
        }
    }
}
