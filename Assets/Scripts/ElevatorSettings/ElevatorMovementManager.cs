using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElevatorSettings{
    [RequireComponent(typeof(ElevatorParameters))]
    [RequireComponent(typeof(ElevatorSoundController))]
    public class ElevatorMovementManager : SerializedMonoBehaviour{
        
        private ElevatorParameters _elevatorParameters;
        private ElevatorSoundController _elevatorSoundController;
        
        private void Awake(){
            _elevatorSoundController = GetComponent<ElevatorSoundController>();
            _elevatorParameters = GetComponent<ElevatorParameters>();
            foreach (var interactibleButton in _elevatorParameters.ElevatorButtons){
                interactibleButton.SetElevatorScripts(this,_elevatorParameters);
            }
            _elevatorParameters.ElevatorPhotocell.SetElevatorScripts(this,_elevatorParameters);
        }

        public void ElevatorButtonClicked(int floorIndex){
            if (floorIndex == _elevatorParameters.CurrentlyActiveFloor){
                if (!_elevatorParameters.DoorsOpen){
                    StartCoroutine(OpenDoors());
                }
                return;
            }
            
            if (!_elevatorParameters.DoorsOpen){
                StartCoroutine(MoveElevatorToTargetTransform(floorIndex));
                return;
            }
            StopAllCoroutines();
            StartCoroutine(MoveElevatorAfterClosingDoors(floorIndex));
        }

        private IEnumerator MoveElevatorToTargetTransform(int floorIndex){
            _elevatorSoundController.PlayOnElevatorStartSound();
            _elevatorParameters.ElevatorIsMoving = true;
            StartCoroutine(StartPlayingMusicAfterDelay());
            var targetTransform = _elevatorParameters.FloorTransforms[floorIndex];
            var targetPosition = targetTransform.position;
            var elevatorPosition = _elevatorParameters.ElevatorRigidbody.transform.position;
            while (elevatorPosition != targetPosition){
                elevatorPosition = _elevatorParameters.ElevatorRigidbody.transform.position;
                _elevatorParameters.ElevatorRigidbody.MovePosition(Vector3.MoveTowards(elevatorPosition,targetPosition,
                    Time.fixedDeltaTime * _elevatorParameters.ElevatorSpeed));
                yield return null;
            }
            _elevatorSoundController.StopElevatorMusic();
            StartCoroutine(OpenDoors());
            _elevatorParameters.ElevatorIsMoving = false;
            _elevatorParameters.CurrentlyActiveFloor = floorIndex;
            _elevatorSoundController.PlayOnFloorArrivalSound();
        }

        private IEnumerator StartPlayingMusicAfterDelay(){
            yield return new WaitForSeconds(_elevatorParameters.TimeToStartMusic);
            _elevatorSoundController.StartElevatorMusic();
        }

        private IEnumerator MoveElevatorAfterClosingDoors(int floorIndex){
            yield return CloseDoors();
            StartCoroutine(MoveElevatorToTargetTransform(floorIndex));
        }

        private IEnumerator OpenDoors(){
            _elevatorParameters.DoorsMoving = true;
            _elevatorParameters.DoorAnimator.SetTrigger("OpenDoors");
            yield return new WaitForSeconds(_elevatorParameters.DoorAnimationLength);
            _elevatorParameters.DoorsOpen = true;
            _elevatorParameters.DoorsMoving = false;
            StartCoroutine(WaitForClosingDoors());
        }

        public void OpenClosingDoors(){
            _elevatorParameters.DoorAnimator.SetTrigger("OpenClosingDoors");
            _elevatorParameters.DoorsOpen = true;
            _elevatorParameters.DoorsClosing = false;
            StartCoroutine(WaitForClosingDoors());
        }

        private IEnumerator WaitForClosingDoors(){
            yield return new WaitForSeconds(_elevatorParameters.TimeToCloseDoors);
            StartCoroutine(CloseDoors());
        }

        private IEnumerator CloseDoors(){
            _elevatorParameters.DoorsMoving = true;
            _elevatorParameters.DoorsClosing = true;
            _elevatorParameters.DoorAnimator.SetTrigger("CloseDoors");
            yield return new WaitForSeconds(_elevatorParameters.DoorAnimationLength);
            _elevatorParameters.DoorsClosing = false;
            _elevatorParameters.DoorsOpen = false;
            _elevatorParameters.DoorsMoving = false;
        }
    }
}
