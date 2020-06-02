using System;
using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace ElevatorSettings{
    public class ElevatorSettings : MonoBehaviour{
        [SerializeField] private float elevatorSpeed;
        [SerializeField] private List<InteractibleButton> elevatorButtons;
        private Rigidbody _elevatorRigidbody;
        private bool _elevatorIsMoving;

        private void Awake(){
            foreach (var interactibleButton in elevatorButtons){
                interactibleButton.ElevatorSettings = this;
            }
        }

        private void Start(){
            _elevatorRigidbody = GetComponent<Rigidbody>();
        }

        public float ElevatorSpeed => elevatorSpeed;
        public Rigidbody ElevatorRigidbody => _elevatorRigidbody;

        public bool ElevatorIsMoving{
            get => _elevatorIsMoving;
            set => _elevatorIsMoving = value;
        }
    }
}
