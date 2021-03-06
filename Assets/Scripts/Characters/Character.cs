using Level;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Characters
{
    [RequireComponent(typeof(CharacterAnimator), typeof(CharacterSkin), typeof(NavMeshAgent))]
    public class Character : MonoBehaviour
    {
        public ECharacterState State = ECharacterState.Free;

        [SerializeField] private int _runSpeed = 15;
        
        private PlayerInfo _leader;

        private NavMeshAgent _agent;

        private CharacterSkin _skin;

        private Vector2 _movableArea;

        private CharacterAnimator _characterAnimator;

        private Vector2 _offset;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _skin = GetComponent<CharacterSkin>();
            _characterAnimator = GetComponent<CharacterAnimator>();
        }

        private void Start()
        {
            _movableArea = LevelManager.Instance.MovableArea;
            _characterAnimator.ChangeAnimationState(CharacterAnimator.ECharacterAnimationState.Walk);
            
            LevelManager.Instance.OnTimeOut += () =>
                _characterAnimator.ChangeAnimationState(CharacterAnimator.ECharacterAnimationState.Idle);
        }

        private void Update()
        {
            switch (State)
            {
                case ECharacterState.Captured: CapturedMove(); break;
                case ECharacterState.Free: FreeMove(); break; 
            }
        }

        public bool TryCapture(PlayerInfo playerInfo)
        {
            if (_leader != null && playerInfo.Crowd.CharactersCount < _leader.Crowd.CharactersCount || _leader == playerInfo)
                return false;
        
            if (_leader != null)
                _leader.Crowd.RemoveCharacter(this);
        
            _leader = playerInfo;
            State = ECharacterState.Captured;

            _agent.speed = _runSpeed;
            
            _offset.y = _leader.Crowd.CharactersCount * 0.1f;
            _offset.x = Random.Range(-3, 3);

            _characterAnimator.ChangeAnimationState(CharacterAnimator.ECharacterAnimationState.Run);
            _skin.SetColor(_leader.Color);

            return true;
        }

        private void FreeMove()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance + 0.5f)
            {
                Vector3 newPos = new Vector3(Random.Range(-_movableArea.x, _movableArea.x), 0, Random.Range(-_movableArea.y, _movableArea.y));
                _agent.SetDestination(newPos);
            }
        }

        private void CapturedMove()
        {
            Vector3 pos = _leader.transform.position - _leader.transform.forward * _offset.y + _leader.transform.right * _offset.x;
            _agent.SetDestination(pos);
        } 
    
        public enum ECharacterState
        {
            Free,
            Captured
        }
    }
}