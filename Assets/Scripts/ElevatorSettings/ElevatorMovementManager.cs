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
            InitializeFields();
            SetElevatorScriptsInInteractibleButtons();
            SetElevatorScriptsInElevatorPhotocell();
        }

        private void InitializeFields(){
            _elevatorSoundController = GetComponent<ElevatorSoundController>();
            _elevatorParameters = GetComponent<ElevatorParameters>();
        }

        private void SetElevatorScriptsInInteractibleButtons(){
            foreach (var interactibleButton in _elevatorParameters.ElevatorButtons){
                interactibleButton.SetElevatorScripts(this, _elevatorParameters);
            }
        }

        private void SetElevatorScriptsInElevatorPhotocell(){
            _elevatorParameters.ElevatorPhotocell.SetElevatorScripts(this, _elevatorParameters);
        }

        public void ElevatorButtonClicked(int floorIndex){
            Debug.Log(_elevatorParameters.DoorsOpen);
            if (CheckIfTargetFloorIndexEqualsCurrentlyActiveFloor(floorIndex)){
                OpenDoorsIfDoorsClosed();
                return;
            }
            
            if (CheckIfDoorsClosed()){
                StartElevatorMovementToTargetFloorIndex(floorIndex);
                return;
            }
            StopAllCoroutinesCloseDoorsAndMoveElevator(floorIndex);
        }

        private bool CheckIfTargetFloorIndexEqualsCurrentlyActiveFloor(int floorIndex){
            return floorIndex == _elevatorParameters.CurrentlyActiveFloor;
        }

        private void OpenDoorsIfDoorsClosed(){
            if (CheckIfDoorsClosed()){
                StartCoroutine(OpenDoors());
            }
        }

        private bool CheckIfDoorsClosed(){
            return !_elevatorParameters.DoorsOpen;
        }

        private void StartElevatorMovementToTargetFloorIndex(int floorIndex){
            StartCoroutine(MoveElevatorToTargetMoveIndex(floorIndex));
        }

        private void StopAllCoroutinesCloseDoorsAndMoveElevator(int floorIndex){
            StopAllCoroutines();
            StartCoroutine(MoveElevatorAfterClosingDoors(floorIndex));
        }
        
        private IEnumerator MoveElevatorAfterClosingDoors(int floorIndex){
            yield return CloseDoors();
            StartElevatorMovementToTargetFloorIndex(floorIndex);
        }

        private IEnumerator MoveElevatorToTargetMoveIndex(int floorIndex){
            ManageElevatorOnMovementStart();
            var targetPosition = SetupParametersForMovement(floorIndex, out var elevatorPosition);
            while (elevatorPosition != targetPosition){
                elevatorPosition = _elevatorParameters.ElevatorRigidbody.transform.position;
                MoveElevatorToTargetPosition(elevatorPosition, targetPosition);
                yield return null;
            }
            ReactAfterReachingDestinationFloor(floorIndex);
        }

        private void ManageElevatorOnMovementStart(){
            ManageSoundOnMovementStart();
            ManageParametersOnMovementStart();
        }

        private void ManageSoundOnMovementStart(){
            _elevatorSoundController.PlayOnElevatorStartSound();
            StartCoroutine(StartPlayingMusicAfterDelay());
        }

        private void ManageParametersOnMovementStart(){
            _elevatorParameters.ElevatorIsMoving = true;
        }

        private Vector3 SetupParametersForMovement(int floorIndex, out Vector3 elevatorPosition){
            var targetTransform = _elevatorParameters.FloorTransforms[floorIndex];
            var targetPosition = targetTransform.position;
            elevatorPosition = _elevatorParameters.ElevatorRigidbody.transform.position;
            return targetPosition;
        }

        private void MoveElevatorToTargetPosition(Vector3 elevatorPosition, Vector3 targetPosition){
            _elevatorParameters.ElevatorRigidbody.MovePosition(Vector3.MoveTowards(elevatorPosition, targetPosition,
                Time.fixedDeltaTime * _elevatorParameters.ElevatorSpeed));
        }

        private void ReactAfterReachingDestinationFloor(int floorIndex){
            ManageSoundAfterReachingDestinationFloor();
            StartCoroutine(OpenDoors());
            ManageParametersAfterReachingDestinationFloor(floorIndex);
        }

        private void ManageSoundAfterReachingDestinationFloor(){
            _elevatorSoundController.StopElevatorMusic();
            _elevatorSoundController.PlayOnFloorArrivalSound();
        }

        private void ManageParametersAfterReachingDestinationFloor(int floorIndex){
            _elevatorParameters.ElevatorIsMoving = false;
            _elevatorParameters.CurrentlyActiveFloor = floorIndex;
        }

        private IEnumerator StartPlayingMusicAfterDelay(){
            yield return new WaitForSeconds(_elevatorParameters.TimeToStartMusic);
            _elevatorSoundController.StartElevatorMusic();
        }


        private IEnumerator OpenDoors(){
            SetParametersOnDoorsOpen();
            yield return new WaitForSeconds(_elevatorParameters.DoorAnimationLength);
            ManageElevatorAfterDoorsOpen();
        }

        private void SetParametersOnDoorsOpen(){
            _elevatorParameters.DoorsMoving = true;
            _elevatorParameters.DoorAnimator.SetTrigger("OpenDoors");
        }

        private void ManageElevatorAfterDoorsOpen(){
            SetParametersAfterDoorsOpen();
            StartCoroutine(WaitForClosingDoors());
        }

        private void SetParametersAfterDoorsOpen(){
            _elevatorParameters.DoorsOpen = true;
            _elevatorParameters.DoorsMoving = false;
        }

        public void OpenClosingDoors(){
            SetParametersOnOpeningClosingDoors();
            StartCoroutine(WaitForClosingDoors());
        }

        private void SetParametersOnOpeningClosingDoors(){
            _elevatorParameters.DoorAnimator.SetTrigger("OpenClosingDoors");
            _elevatorParameters.DoorsOpen = true;
            _elevatorParameters.DoorsClosing = false;
        }

        private IEnumerator WaitForClosingDoors(){
            yield return new WaitForSeconds(_elevatorParameters.TimeToCloseDoors);
            StartCoroutine(CloseDoors());
        }

        private IEnumerator CloseDoors(){
            SetParametersOnClosingDoors();
            yield return new WaitForSeconds(_elevatorParameters.DoorAnimationLength);
            SetParametersAfterClosingDoors();
        }

        private void SetParametersOnClosingDoors(){
            _elevatorParameters.DoorsMoving = true;
            _elevatorParameters.DoorsClosing = true;
            _elevatorParameters.DoorAnimator.SetTrigger("CloseDoors");
        }

        private void SetParametersAfterClosingDoors(){
            _elevatorParameters.DoorsClosing = false;
            _elevatorParameters.DoorsOpen = false;
            _elevatorParameters.DoorsMoving = false;
        }
    }
}
