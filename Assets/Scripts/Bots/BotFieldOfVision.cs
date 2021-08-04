using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bots
{
    public class BotFieldOfVision : MonoBehaviour
    {
        public Action<PlayerInfo> OnAvailablePursuitPlayer;
        public Action OnLostPursuitPlayer;
        
        private List<PlayerInfo> _visibleBots = new List<PlayerInfo>();

        private PlayerInfo _playerInfo;
        private PlayerInfo _pursuitPlayer;

        private void Awake()
        {
            _playerInfo = GetComponentInParent<PlayerInfo>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInfo playerInfo))
            {
                _visibleBots.Add(playerInfo);
                CheckForPursuitPlayer();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerInfo playerInfo))
            {
                _visibleBots.Remove(playerInfo);

                if (playerInfo == _pursuitPlayer)
                {
                    OnLostPursuitPlayer?.Invoke();
                    CheckForPursuitPlayer();
                }
            }
        }

        private void CheckForPursuitPlayer()
        {
            foreach (var bot in _visibleBots)
            {
                if (bot.Crowd.CharactersCount < _playerInfo.Crowd.CharactersCount)
                {
                    _pursuitPlayer = bot;
                    OnAvailablePursuitPlayer?.Invoke(_pursuitPlayer);
                    return;
                }
            }
        }
    }
}
