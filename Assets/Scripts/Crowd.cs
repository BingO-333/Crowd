using System;
using System.Collections.Generic;
using Level;
using UI;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public Action<int> OnCharacterCountChange;
    public Action<int> OnKill;

    public Action<PlayerInfo, PlayerInfo> OnDie; // killed , killer

    public int CharactersCount => _characters.Count + 1;
    
    [HideInInspector] public PlayerInfo Player;


    private List<Character.Character> _characters = new List<Character.Character>();

    private int _kills;
    
    private bool _isActive;

    private void Awake()
    {
        Player = GetComponentInParent<PlayerInfo>();
    }

    private void Start()
    {
        LevelManager.Instance.UIManager.InstantiateCharacterCountView(this);

        LevelManager.Instance.OnStartGame += Active;
        
        LevelManager.Instance.OnTimeOut += SaveScore;
        LevelManager.Instance.OnTimeOut += Inactive;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnStartGame -= Active;
        
        LevelManager.Instance.OnTimeOut -= SaveScore;
        LevelManager.Instance.OnTimeOut -= Inactive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
            return;
        
        if (other.TryGetComponent(out Character.Character character) && character.TryCapture(Player))
            AddCharacter(character);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isActive)
            return;
        
        if (other.TryGetComponent(out Crowd crowd) && crowd.CharactersCount <= 1 && CharactersCount > 1)
        {
            _kills++;
            OnKill?.Invoke(_kills);

            crowd.Die(Player);
        }
    }

    public void RemoveCharacter(Character.Character character)
    {
        if (_characters.Contains(character))
        {
            _characters.Remove(character);
            OnCharacterCountChange?.Invoke(CharactersCount);
        }
    }

    private void Die(PlayerInfo killer) => OnDie?.Invoke(Player, killer);

    private void Active() => _isActive = true;

    private void Inactive() => _isActive = false;

    private void AddCharacter(Character.Character character)
    {
        _characters.Add(character);
        OnCharacterCountChange?.Invoke(CharactersCount);
    }

    private void SaveScore()
    {
        if (PlayerPrefs.GetInt("BestScore", 0) < CharactersCount)
            PlayerPrefs.SetInt("BestScore", CharactersCount);
    }
}