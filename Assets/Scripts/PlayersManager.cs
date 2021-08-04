using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Level;
using Level;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayersManager : MonoBehaviour
{
    public List<PlayerInfo> Players = new List<PlayerInfo>();
    [HideInInspector] public PlayerInfo Player;

    [SerializeField] private Data _data;
    [SerializeField] private SpawnerByArea[] _spawners;

    [Space]
    [SerializeField] private PlayerInfo _playerPrefab;
    [SerializeField] private PlayerInfo _botPrefab;

    [Space]
    [SerializeField] private int _botsCount = 5;

    private Color[] _colors = new Color[0];
    private string[] _nicknames;

    private void Awake()
    {
        Instance();
    }

    private void Start()
    {
        LevelManager.Instance.OnStartGame += () => Player.SetNickname(PlayerPrefs.GetString("Nickname", "Player"));
    }

    public Dictionary<PlayerInfo, int> GetTopPlayers()
    {
        PlayerInfo[] topPlayers = Players.OrderByDescending(p => p.Crowd.CharactersCount).ToArray();

        Dictionary<PlayerInfo, int> dictionaryPlayers = new Dictionary<PlayerInfo, int>();
            
        for (int i = 0; i < topPlayers.Length; i++)
        {
            dictionaryPlayers.Add(topPlayers[i], i + 1);
        }

        return dictionaryPlayers;
    }

    private void Instance()
    {
        _nicknames = _data.Nicknames.OrderBy(n => Random.value).ToArray();
        _colors = _data.Colors.OrderBy(c => Random.value).ToArray();
        
        SpawnPlayer();
        SpawnBots();
    }

    private void SpawnBots()
    {
        if (_colors.Length < _botsCount + 1)
            Debug.LogWarning("Need more colors");
        
        if (_nicknames.Length < _botsCount)
            Debug.LogWarning("Need more nicknames");
        
        for (int i = 0; i < _botsCount; i++)
        {
            PlayerInfo botInfo = Instantiate(_botPrefab, GetSpawnPosition(), Quaternion.identity);
            
            botInfo.SetNickname(_nicknames[i]);
            botInfo.SetColor(_colors[i + 1]);

            botInfo.Crowd.OnDie += OnPlayerDeath;
            
            Players.Add(botInfo);
        }
    }
    
    private void SpawnPlayer()
    {
        Player = Instantiate(_playerPrefab, GetSpawnPosition(), Quaternion.identity);
        
        Player.SetNickname(PlayerPrefs.GetString("Nickname", "Player"));
        Player.SetColor(_colors[0]);

        Player.Crowd.OnDie += (killed, killer) => killed.gameObject.SetActive(false);
        
        Players.Add(Player);
    }

    private Vector3 GetSpawnPosition()
    {
        if (_spawners.Length == 0)
        {
            Debug.LogWarning("NO SPAWNERS !!!");
            return Vector3.zero;
        }
        
        Vector3 randomPosition = _spawners[Random.Range(0, _spawners.Length)].GetRandomPosition();
        return randomPosition;
    }

    private void OnPlayerDeath(PlayerInfo killed, PlayerInfo killer) => killed.transform.position = GetSpawnPosition();
}
