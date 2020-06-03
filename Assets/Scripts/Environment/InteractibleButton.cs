using ElevatorSettings;
using UnityEngine;

namespace Environment{
    public class InteractibleButton : MonoBehaviour{
        [SerializeField] private int floorIndex;
        private ElevatorMovementManager _elevatorMovementManager;
        private ElevatorParameters _elevatorParameters;

        public void SetElevatorScripts(ElevatorMovementManager elevatorMovementManager,
                                       ElevatorParameters elevatorParameters){
            _elevatorMovementManager = elevatorMovementManager;
            _elevatorParameters = elevatorParameters;
        }

        public virtual void Interact(){
            if (_elevatorParameters.ElevatorIsMoving || _elevatorParameters.DoorsMoving){ return; }
            _elevatorMovementManager.ElevatorButtonClicked(floorIndex);
        }
    }
}
