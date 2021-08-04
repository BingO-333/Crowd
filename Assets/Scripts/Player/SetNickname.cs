using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class SetNickname : MonoBehaviour
    {
        private InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<InputField>();

            _inputField.text = PlayerPrefs.GetString("Nickname", "Player");
        }

        public void SaveNickname(string nick) => PlayerPrefs.SetString("Nickname", nick);
    }
}
