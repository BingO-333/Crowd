using Level;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3;
        [SerializeField] private float _rotateSpeed = 5;

        private InputController _inputController;

        private CharacterController _characterController;
        
        private Quaternion _rotateDirection;

        private bool _canMove;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            
            _inputController = InputController.Instance;

            LevelManager.Instance.OnStartGame += StartMove;
            LevelManager.Instance.OnTimeOut += StopMove;

            LevelManager.Instance.PlayersManager.Player.Crowd.OnDie += (info, playerInfo) => _canMove = false;
        }

        private void Update()
        {
            if (_canMove)
            {
                Move();
                Rotate(_inputController.SwipeDirection);
            }
        }

        private void StartMove() => _canMove = true;
        private void StopMove() => _canMove = false;

        private void Move()
        {
            _characterController.Move(transform.forward * (_moveSpeed * Time.deltaTime));
        }

        private void Rotate(Vector2 direction)
        {
            if (direction.magnitude != 0)
                _rotateDirection = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
            
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotateDirection, _rotateSpeed * Time.deltaTime);
        }
    }
}
