using System;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeView : MonoBehaviour
    {
        private LevelManager _level;

        private Text _time;

        private void Awake()
        {
            _time = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            _level = LevelManager.Instance;
            UpdateTimeView();
        }

        private void Update()
        {
            UpdateTimeView();
        }

        private void UpdateTimeView()
        {
            int minutes = (int)_level.TimeRemain / 60;
            int seconds = (int) _level.TimeRemain % 60;

            string secondsText = seconds < 10 ? $"0{seconds}" : seconds.ToString();

            _time.text = $"{minutes}:{secondsText}";
        }
    }
}