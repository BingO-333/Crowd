using System;
using Level;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Bots
{
    [RequireComponent(typeof(PlayerInfo), typeof(NavMeshAgent))]
    public class BotMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private BotFieldOfVision _fieldOfVision;

        private PlayerInfo _botInfo;
        private PlayerInfo _persecutionTarget;
        
        private Vector2 _movableArea;

        private EBotMoveState _state = EBotMoveState.Stay;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _botInfo = GetComponent<PlayerInfo>();

            _fieldOfVision = GetComponentInChildren<BotFieldOfVision>();
        }

        private void Start()
        {
            _fieldOfVision.OnAvailablePursuitPlayer += StartPersecution;
            _fieldOfVision.OnLostPursuitPlayer += StopPersecution;
            
            LevelManager.Instance.OnStartGame += () => ChangeState(EBotMoveState.Move);
            LevelManager.Instance.OnTimeOut += () => ChangeState(EBotMoveState.Stay);

            _movableArea = LevelManager.Instance.MovableArea;
        }

        private void Update()
        {
            switch (_state)
            {
                case EBotMoveState.Move: FreeMove(); break;
                case EBotMoveState.Persecution: Persecution(); break;
            }
        }

        private void FreeMove()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance + 0.5f)
            {
                Vector3 newPos = new Vector3(Random.Range(-_movableArea.x, _movableArea.x), 0, Random.Range(-_movableArea.y, _movableArea.y));
                _agent.SetDestination(newPos);
            }
        }

        private void Persecution()
        {
            _agent.SetDestination(_persecutionTarget.transform.position);
            
            if (_persecutionTarget.Crowd.CharactersCount >= _botInfo.Crowd.CharactersCount || _agent.remainingDistance <= 0.5f)
                StopPersecution();
        }

        private void StartPersecution(PlayerInfo target)
        {
            _persecutionTarget = target;
            _state = EBotMoveState.Persecution;
        }

        private void StopPersecution()
        {
            _state = EBotMoveState.Move;
            _persecutionTarget = null;
        }
        
        private void ChangeState(EBotMoveState state) => _state = state;

        private enum EBotMoveState
        {
            Stay,
            Move,
            Persecution
        }
    }
}
