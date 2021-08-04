using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharactersCountView : MonoBehaviour
    {
        [SerializeField] private float _height = 5;
        [Space]
        [SerializeField] private Image[] _bgs;
        [SerializeField] private Text _count;

        private RectTransform _rectTransform;
        private Transform _target;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;

            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_target == null)
                return;
            
            Vector3 pos = _camera.WorldToScreenPoint(_target.position + Vector3.up * _height);
            _rectTransform.transform.position = pos;
        }

        public void Instance(Crowd crowd)
        {
            crowd.OnCharacterCountChange += UpdateCount;
            
            SetColor(crowd.Player.Color);

            _target = crowd.transform;
        }

        private void SetColor(Color color)
        {
            foreach (var bg in _bgs)
                bg.color = color;
        }

        private void UpdateCount(int count)
        {
            _count.text = count.ToString();
        }
    }
}
