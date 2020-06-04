using System.Collections.Generic;
using Environment;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ElevatorSettings{
    public class ElevatorParameters : SerializedMonoBehaviour{
        [SerializeField] private float elevatorSpeed;
        [SerializeField] private List<InteractibleButton> elevatorButtons;
        [SerializeField] private ElevatorPhotocell elevatorPhotocell;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private float doorAnimationLength;
        [SerializeField] private float timeToCloseDoors;
        [SerializeField] private float timeToStartMusic;
        [SerializeField] private Dictionary<int, Transform> floorTransforms;
        private int _currentlyActiveFloor = 3;
        private Rigidbody _elevatorRigidbody;
        private bool _elevatorIsMoving;
        private bool _doorsOpen = true;
        private bool _doorsClosing;
        private bool _doorsMoving;
        
        private void Awake(){
            InitializeFields();
        }

        private void InitializeFields(){
            _elevatorRigidbody = GetComponent<Rigidbody>();
        }

        public float ElevatorSpeed => elevatorSpeed;

        public List<InteractibleButton> ElevatorButtons => elevatorButtons;

        public ElevatorPhotocell ElevatorPhotocell => elevatorPhotocell;

        public Animator DoorAnimator => doorAnimator;

        public float DoorAnimationLength => doorAnimationLength;

        public float TimeToCloseDoors => timeToCloseDoors;

        public float TimeToStartMusic => timeToStartMusic;

        public Dictionary<int, Transform> FloorTransforms => floorTransforms;

        public Rigidbody ElevatorRigidbody => _elevatorRigidbody;

        public int CurrentlyActiveFloor{
            get => _currentlyActiveFloor;
            set => _currentlyActiveFloor = value;
        }

        public bool ElevatorIsMoving{
            get => _elevatorIsMoving;
            set => _elevatorIsMoving = value;
        }

        public bool DoorsOpen{
            get => _doorsOpen;
            set => _doorsOpen = value;
        }

        public bool DoorsClosing{
            get => _doorsClosing;
            set => _doorsClosing = value;
        }

        public bool DoorsMoving{
            get => _doorsMoving;
            set => _doorsMoving = value;
        }
    }
}
