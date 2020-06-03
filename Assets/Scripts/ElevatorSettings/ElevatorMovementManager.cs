using System;
using System.Collections;
using System.Collections.Generic;
using Environment;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElevatorSettings{
    public class ElevatorMovementManager : SerializedMonoBehaviour{
        [SerializeField] private float elevatorSpeed;
        [SerializeField] private List<InteractibleButton> elevatorButtons;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private float doorAnimationLength;
        [SerializeField] private float timeToCloseDoors;
        [SerializeField] private Dictionary<int, Transform> floorTransforms;
        private int _currentlyActiveFloor = 3;
        private Rigidbody _elevatorRigidbody;
        private bool _elevatorIsMoving;
        private bool _doorsOpen = true;
        private bool _doorsClosing;
        private bool _doorsMoving;
        
        private void Awake(){
            foreach (var interactibleButton in elevatorButtons){
                interactibleButton.ElevatorMovementManager = this;
            }
        }

        private void Start(){
            _elevatorRigidbody = GetComponent<Rigidbody>();
        }

        public void ElevatorButtonClicked(int floorIndex){
            if (floorIndex == _currentlyActiveFloor){
                if (!_doorsOpen){
                    StartCoroutine(OpenDoors());
                }
                return;
            }
            
            if (!_doorsOpen){
                StartCoroutine(MoveElevatorToTargetTransform(floorIndex));
                return;
            }
            StopAllCoroutines();
            StartCoroutine(MoveElevatorAfterClosingDoors(floorIndex));
        }

        private IEnumerator MoveElevatorToTargetTransform(int floorIndex){
            _elevatorIsMoving = true;
            var targetTransform = floorTransforms[floorIndex];
            var targetPosition = targetTransform.position;
            var elevatorPosition = _elevatorRigidbody.transform.position;
            while (elevatorPosition != targetPosition){
                elevatorPosition = _elevatorRigidbody.transform.position;
                _elevatorRigidbody.MovePosition(Vector3.MoveTowards(elevatorPosition,targetPosition,
                    Time.fixedDeltaTime * elevatorSpeed));
                yield return null;
            }
            StartCoroutine(OpenDoors());
            _elevatorIsMoving = false;
            _currentlyActiveFloor = floorIndex;
        }

        private IEnumerator MoveElevatorAfterClosingDoors(int floorIndex){
            yield return CloseDoors();
            StartCoroutine(MoveElevatorToTargetTransform(floorIndex));
        }

        private IEnumerator OpenDoors(){
            _doorsMoving = true;
            doorAnimator.SetTrigger("OpenDoors");
            yield return new WaitForSeconds(doorAnimationLength);
            _doorsOpen = true;
            _doorsMoving = false;
            StartCoroutine(WaitForClosingDoors());
        }

        public void OpenClosingDoors(){
            doorAnimator.SetTrigger("OpenClosingDoors");
            _doorsOpen = true;
            _doorsClosing = false;
            StartCoroutine(WaitForClosingDoors());
        }

        private IEnumerator WaitForClosingDoors(){
            yield return new WaitForSeconds(timeToCloseDoors);
            StartCoroutine(CloseDoors());
        }

        private IEnumerator CloseDoors(){
            _doorsMoving = true;
            _doorsClosing = true;
            doorAnimator.SetTrigger("CloseDoors");
            yield return new WaitForSeconds(doorAnimationLength);
            _doorsClosing = false;
            _doorsOpen = false;
            _doorsMoving = false;
        }

        public bool ElevatorIsMoving => _elevatorIsMoving;
        public bool DoorsOpen => _doorsOpen;
        public bool DoorsMoving => _doorsMoving;

        public bool DoorsClosing => _doorsClosing;
    }
}
