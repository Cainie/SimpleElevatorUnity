using System.Collections;
using UnityEngine;

namespace Environment{
    public class InteractibleButton : MonoBehaviour{
        [SerializeField] private float lerpSpeed;
        [SerializeField] private Rigidbody elevatorRigidbody;
        [SerializeField] private Transform targetTransform;
        
        public virtual void Interact(){
        }

        protected IEnumerator MoveElevatorToTargetTransform(){
            var targetPosition = targetTransform.position;
            var elevatorPosition = elevatorRigidbody.transform.position;
            while (elevatorPosition != targetPosition){
                elevatorPosition = elevatorRigidbody.transform.position;
                elevatorRigidbody.MovePosition(Vector3.MoveTowards(elevatorPosition,targetPosition,
                    Time.fixedDeltaTime * lerpSpeed));
                yield return null;
            }
        }
    }
}
