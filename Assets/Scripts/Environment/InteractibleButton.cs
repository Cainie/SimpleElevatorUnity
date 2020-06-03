using System.Collections;
using UnityEngine;

namespace Environment{
    public class InteractibleButton : MonoBehaviour{
        [SerializeField] private int floorIndex;
        private ElevatorSettings.ElevatorMovementManager _elevatorMovementManager;

        public ElevatorSettings.ElevatorMovementManager ElevatorMovementManager{
            set => _elevatorMovementManager = value;
        }

        public virtual void Interact(){
            if (_elevatorMovementManager.ElevatorIsMoving || _elevatorMovementManager.DoorsMoving){ return; }
            _elevatorMovementManager.ElevatorButtonClicked(floorIndex);
        }
    }
}
