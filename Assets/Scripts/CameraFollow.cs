using System;
using Level;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smooth = 4;
    [SerializeField] private float _multiplierPerCharacter = 0.005f;
    [SerializeField] private float _maxMultiplier = 0.5f;

    private PlayersManager _playersManager;
    
    private Transform _target;

    private void Start()
    {
        _playersManager = LevelManager.Instance.PlayersManager;
        
        _target = _playersManager.Player.transform;
            
        transform.position = _target.position + _offset;
    }

    private void Update()
    {
        if (_target == null)
            return;

        float multiplier = Mathf.Clamp(LevelManager.Instance.PlayersManager.Player.Crowd.CharactersCount * _multiplierPerCharacter, 0, _maxMultiplier);
        
        Vector3 multiplyOffset = _offset * (1 + multiplier);
        
        transform.position = Vector3.Lerp(transform.position, _target.position + multiplyOffset, _smooth * Time.deltaTime);
    }
}
