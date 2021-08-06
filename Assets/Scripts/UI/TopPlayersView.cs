using System;
using System.Linq;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TopPlayersView : MonoBehaviour
    {
        [SerializeField] private Text _top1, _top2, _top3;

        private PlayersManager _playersManager;

        private void Start()
        {
            _playersManager = LevelManager.Instance.PlayersManager;

            foreach (PlayerInfo player in _playersManager.Players)
                player.Crowd.OnCharacterCountChange += UpdateUI;
        }

        private void UpdateUI(int value)
        {
            PlayerInfo[] topPlayers = _playersManager.Players.OrderByDescending(p => p.Crowd.CharactersCount).ToArray();

            _top1.text = $"#1 x {topPlayers[0].Crowd.CharactersCount} {topPlayers[0].Nickname}";
            _top1.color = topPlayers[0].Color;
            
            if (topPlayers.Length < 2)
                return;
            
            _top2.text = $"#2 x {topPlayers[1].Crowd.CharactersCount} {topPlayers[1].Nickname}";
            _top2.color = topPlayers[1].Color;
            
            if (topPlayers.Length < 3)
                return;
            
            _top3.text = $"#3 x {topPlayers[2].Crowd.CharactersCount} {topPlayers[2].Nickname}";
            _top3.color = topPlayers[2].Color;
        }
    }
}
