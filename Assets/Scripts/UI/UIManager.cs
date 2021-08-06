using System;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject
            _menuScreen,
            _levelScreen,
            _killedScreen,
            _timeOutScreen;

        [Header("Killed screen")]
        [SerializeField] private Text _killer;
        [SerializeField] private Text _placeKS;

        [Header("TimeOutScreen")]
        [SerializeField] private Text _placeTS;
        [SerializeField] private Text _crowdSize;
        [SerializeField] private Text _nickname;

        [Space]
        [SerializeField] private CharactersCountView _charactersCountViewPrefab;
        [SerializeField] private Transform _charactersCountViewPanel;

        private void Start()
        {
            LevelManager.Instance.PlayersManager.Player.Crowd.OnDie += OpenKilledScreen;

            LevelManager.Instance.OnTimeOut += OpenTimeOutScreen;
        }

        public void InstantiateCharacterCountView(Crowd crowd)
        {
            CharactersCountView view = Instantiate(_charactersCountViewPrefab, _charactersCountViewPanel);
            view.Instance(crowd);
        }

        public void OpenLevelScreen()
        {
            _menuScreen.SetActive(false);
            _levelScreen.SetActive(true);
        }

        private void OpenTimeOutScreen()
        {
            PlayerInfo player = LevelManager.Instance.PlayersManager.Player;
            
            _placeTS.text = "# " + LevelManager.Instance.PlayersManager.GetTopPlayers()[player];
            _crowdSize.text = player.Crowd.CharactersCount.ToString();

            _nickname.text = player.Nickname;
            _nickname.color = player.Color;
            
            _levelScreen.SetActive(false);
            _timeOutScreen.SetActive(true);
        }

        private void OpenKilledScreen(PlayerInfo killed, PlayerInfo killer)
        {
            _killer.text = killer.Nickname;
            _killer.color = killer.Color;

            _placeKS.text = "# " + LevelManager.Instance.PlayersManager.GetTopPlayers()[killed];
            
            _levelScreen.SetActive(false);
            _killedScreen.SetActive(true);
        }
    }
}
