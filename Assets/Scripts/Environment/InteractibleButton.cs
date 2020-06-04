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
            InitializeFields();
        }

        private void InitializeFields(){
            _buttonAudioSource = GetComponent<AudioSource>();
        }

        public void SetElevatorScripts(ElevatorMovementManager elevatorMovementManager,
                                       ElevatorParameters elevatorParameters){
            _elevatorMovementManager = elevatorMovementManager;
            _elevatorParameters = elevatorParameters;
        }

        public virtual void Interact(){
            PlayButtonClickedSound();
            if (CheckIfElevatorStateAllowsButtonClick()){ return; }
            ButtonClicked();
        }

        private void PlayButtonClickedSound(){
            _buttonAudioSource.PlayOneShot(clickingAudioClip);
        }

        private bool CheckIfElevatorStateAllowsButtonClick(){
            return _elevatorParameters.ElevatorIsMoving || _elevatorParameters.DoorsMoving;
        }

        private void ButtonClicked(){
            _elevatorMovementManager.ElevatorButtonClicked(floorIndex);
        }
    }
}
