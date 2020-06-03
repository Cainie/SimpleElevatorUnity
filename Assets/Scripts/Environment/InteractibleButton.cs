using ElevatorSettings;
using UnityEngine;

namespace Environment{
    [RequireComponent(typeof(AudioSource))]
    public class InteractibleButton : MonoBehaviour{
        [SerializeField] private int floorIndex;
        [SerializeField] private AudioClip clickingAudioClip;
        private AudioSource _buttonAudioSource;
        private ElevatorMovementManager _elevatorMovementManager;
        private ElevatorParameters _elevatorParameters;

        private void Awake(){
            _buttonAudioSource = GetComponent<AudioSource>();
        }

        public void SetElevatorScripts(ElevatorMovementManager elevatorMovementManager,
                                       ElevatorParameters elevatorParameters){
            _elevatorMovementManager = elevatorMovementManager;
            _elevatorParameters = elevatorParameters;
        }

        public virtual void Interact(){
            _buttonAudioSource.PlayOneShot(clickingAudioClip);
            if (_elevatorParameters.ElevatorIsMoving || _elevatorParameters.DoorsMoving){ return; }
            _elevatorMovementManager.ElevatorButtonClicked(floorIndex);
        }
    }
}
