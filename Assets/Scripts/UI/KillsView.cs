using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KillsView : MonoBehaviour
    {
        [SerializeField] private PlayersManager _playersManager;
        
        private Text _kills;

        private void Awake()
        {
            _kills = GetComponent<Text>();
        }

        private void Start()
        {
            _playersManager.Player.Crowd.OnKill += UpdateKillUI;
        }

        private void UpdateKillUI(int count) => _kills.text = $"KILLS : {count}";
    }
}
