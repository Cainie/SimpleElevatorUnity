using System.Collections;
using UnityEngine;

namespace Environment{
    public class InteractibleButton : MonoBehaviour{
        [SerializeField] private Transform targetTransform;
        private ElevatorSettings.ElevatorManager _elevatorManager;

        public ElevatorSettings.ElevatorManager ElevatorManager{
            set => _elevatorManager = value;
        }

        public virtual void Interact(){
            if (_elevatorManager.ElevatorIsMoving){ return; }
            StartCoroutine(_elevatorManager.MoveElevatorToTargetTransform(targetTransform));
        }
    }
}
