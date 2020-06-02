using System;
using System.Collections;
using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace ElevatorSettings{
    public class ElevatorManager : MonoBehaviour{
        [SerializeField] private float elevatorSpeed;
        [SerializeField] private List<InteractibleButton> elevatorButtons;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private float doorAnimationLength;
        private Rigidbody _elevatorRigidbody;
        private bool _elevatorIsMoving;
        private bool _doorsOpen = true;
        
        private void Awake(){
            foreach (var interactibleButton in elevatorButtons){
                interactibleButton.ElevatorManager = this;
            }
        }

        private void Start(){
            _elevatorRigidbody = GetComponent<Rigidbody>();
        }
        
        public IEnumerator MoveElevatorToTargetTransform(Transform targetTransform){
            if (_doorsOpen){
                CloseDoors();
                yield return WaitForAnimationToFinish();
            }
            _elevatorIsMoving = true;
            var targetPosition = targetTransform.position;
            var elevatorPosition = _elevatorRigidbody.transform.position;
            while (elevatorPosition != targetPosition){
                elevatorPosition = _elevatorRigidbody.transform.position;
                _elevatorRigidbody.MovePosition(Vector3.MoveTowards(elevatorPosition,targetPosition,
                    Time.fixedDeltaTime * elevatorSpeed));
                yield return null;
            }
            OpenDoors();
            yield return WaitForAnimationToFinish();
            _elevatorIsMoving = false;
        }

        private void OpenDoors(){
            doorAnimator.SetTrigger("OpenDoors");
            _doorsOpen = true;
        }

        private void CloseDoors(){
            doorAnimator.SetTrigger("CloseDoors");
            _doorsOpen = false;
        }

        private IEnumerator WaitForAnimationToFinish(){
            yield return new WaitForSeconds(doorAnimationLength);
        }

        public bool ElevatorIsMoving => _elevatorIsMoving;
    }
}
