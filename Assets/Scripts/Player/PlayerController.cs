using System.Collections;
using System.Collections.Generic;
using Environment;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxRaycastDistance;
    
    private void Update(){
        if (Input.GetMouseButtonDown(0)){
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
