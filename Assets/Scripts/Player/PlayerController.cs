using Environment;
using UnityEngine;

namespace Player{
    public class PlayerController : MonoBehaviour{
        [SerializeField] private Transform initialPosition;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float maxRaycastDistance;
    
        private void Update(){
            TeleportPlayerToInitialPositionOnSpacePress();
            if (PlayerNotInteractingWithWorld()){ return;}
            var ray = CreateRaycastToCrosshair();
            if (!Physics.Raycast(ray, out var hit, maxRaycastDistance)){ return;}
            var interactibleHitObject = hit.collider.GetComponent<InteractibleButton>();
            if (CheckIfObjectIsInteractible(interactibleHitObject)){ return;}
            InteractWithInteractibleObject(interactibleHitObject);
        }

        private void TeleportPlayerToInitialPositionOnSpacePress(){
            if (Input.GetKeyDown(KeyCode.Space)){
                transform.position = initialPosition.position;
            }
        }

        private Ray CreateRaycastToCrosshair(){
            return mainCamera.ScreenPointToRay(Input.mousePosition);
        }

        private bool PlayerNotInteractingWithWorld(){
            return !Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.E);
        }

        private bool CheckIfObjectIsInteractible(InteractibleButton interactibleHitObject){
            return interactibleHitObject == null;
        }

        private void InteractWithInteractibleObject(InteractibleButton interactibleHitObject){
            interactibleHitObject.Interact();
        }
    }
}
