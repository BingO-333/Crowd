using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayersManager playersManager;
    [Space]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smooth = 1;

    private Transform _target;

    private void Start()
    {
        _target = playersManager.Player.transform;
            
        transform.position = _target.position + _offset;
    }

    private void Update()
    {
        if (_target == null)
            return;
        
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _smooth * Time.deltaTime);
    }
}
