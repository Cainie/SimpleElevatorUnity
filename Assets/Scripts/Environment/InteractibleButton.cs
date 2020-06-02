using System.Collections;
using UnityEngine;
using ElevatorSettings;

namespace Environment{
    public class InteractibleButton : MonoBehaviour{
        [SerializeField] private Transform targetTransform;
        private ElevatorSettings.ElevatorSettings _elevatorSettings;

        public ElevatorSettings.ElevatorSettings ElevatorSettings{
            set => _elevatorSettings = value;
        }

        public virtual void Interact(){
            if (_elevatorSettings.ElevatorIsMoving){ return; }
            StartCoroutine(MoveElevatorToTargetTransform());
        }
        
        
        protected IEnumerator MoveElevatorToTargetTransform(){
            _elevatorSettings.ElevatorIsMoving = true;
            var targetPosition = targetTransform.position;
            var elevatorPosition = _elevatorSettings.ElevatorRigidbody.transform.position;
            while (elevatorPosition != targetPosition){
                elevatorPosition = _elevatorSettings.ElevatorRigidbody.transform.position;
                _elevatorSettings.ElevatorRigidbody.MovePosition(Vector3.MoveTowards(elevatorPosition,targetPosition,
                    Time.fixedDeltaTime * _elevatorSettings.ElevatorSpeed));
                yield return null;
            }
            _elevatorSettings.ElevatorIsMoving = false;
        }
    }
}
