using System.Collections;
using UnityEngine;

namespace Environment{
    public class ElevatorButton : InteractibleButton{
        
        public override void Interact(){
            StartCoroutine(MoveElevatorToTargetTransform());
        }

        
    }
}
