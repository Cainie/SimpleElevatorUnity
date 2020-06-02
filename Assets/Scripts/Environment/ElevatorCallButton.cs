using System.Collections;
using UnityEngine;

namespace Environment{
    public class ElevatorCallButton : InteractibleButton{
        
        public override void Interact(){
            StartCoroutine(MoveElevatorToTargetTransform());
        }
    }
}
