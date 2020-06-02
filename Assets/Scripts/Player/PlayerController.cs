using Environment;
using UnityEngine;

namespace Player{
    public class PlayerController : MonoBehaviour{
        [SerializeField] private Transform initialPosition;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float maxRaycastDistance;
    
        private void Update(){
            if (Input.GetKeyDown(KeyCode.Space)){
                transform.position = initialPosition.position;
            }
            if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.E)) return;
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, maxRaycastDistance)) return;
            var interactibleHitObject = hit.collider.GetComponent<Interactible>();
            if (interactibleHitObject == null){
                return;
            }
            interactibleHitObject.Interact();
        }
    }
}
