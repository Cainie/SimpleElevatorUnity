using UnityEngine;

namespace Player{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour{

        [SerializeField] private float speed;
        private float _gravityForce = 9.81f;
        private Rigidbody _playerRigidbody;
        private Transform _playerTransform;
    

        private void Awake(){
            InitializeFields();
        }

        private void InitializeFields(){
            _playerTransform = transform;
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate(){
            var moveVector = GetUserMoveVector();
            moveVector = MultiplyMoveVectorBySpeedAndGravity(moveVector);
            ApplyInputToPlayer(moveVector);
        }

        private Vector3 GetUserMoveVector(){
            var xAxis = Input.GetAxis("Horizontal");
            var zAxis = Input.GetAxis("Vertical");
            var moveVector = _playerTransform.right * xAxis + _playerTransform.forward * zAxis;
            return moveVector;
        }

        private Vector3 MultiplyMoveVectorBySpeedAndGravity(Vector3 moveVector){
            moveVector *= speed;
            moveVector += -_playerTransform.up * _gravityForce;
            return moveVector;
        }

        private void ApplyInputToPlayer(Vector3 moveVector){
            _playerRigidbody.velocity = moveVector;
        }
    }
}