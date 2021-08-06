using System;
using Level;
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
        [SerializeField] private RectTransform _pointer;

        private RectTransform _rectTransform;
        private Transform _target;
        private Transform _mainPlayer;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;

            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _mainPlayer = LevelManager.Instance.PlayersManager.Player.transform;
        }

        private void Update()
        {
            if (_target == null)
                return;

            SetPosition();
        }

        public void Instance(Crowd crowd)
        {
            crowd.OnCharacterCountChange += UpdateCount;
            
            SetColor(crowd.Player.Color);

            _target = crowd.transform;
        }

        private void SetPosition()
        {
            Vector3 pos = _camera.WorldToScreenPoint(_target.position + Vector3.up * _height);

            if (pos.z < 0)
            {
                pos *= -1;
            }
            
            if (pos.x <= 0 || pos.x >= Screen.width || pos.y <= 0 || pos.y >= Screen.height)
            {
                Vector2 clampedPos = Vector2.zero;

                float offset = 100;
                
                clampedPos.x = Mathf.Clamp(pos.x, offset, Screen.width - offset);
                clampedPos.y = Mathf.Clamp(pos.y, offset, Screen.height - offset);
            
                _rectTransform.transform.position = clampedPos;
                
                RotatePointer();
            }
            else
            {
                _rectTransform.transform.position = pos;
                
                _pointer.rotation = Quaternion.identity;
            }
        }

        private void RotatePointer()
        {
            Vector3 toPosition = _target.position;
            Vector3 fromPosition = _mainPlayer.position;

            Vector3 dir = (toPosition - fromPosition).normalized;

            float angle = (Mathf.Atan2(dir.x, -dir.z) * Mathf.Rad2Deg) % 360;
            _pointer.localEulerAngles = new Vector3(0, 0, angle);
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
