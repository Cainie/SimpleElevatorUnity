using UnityEngine;

namespace Environment{
    public class Interactible : MonoBehaviour
    {
        public virtual void Interact(){
            Debug.Log("Interacting");
        }
    }
}
