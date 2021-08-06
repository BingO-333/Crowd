using Characters;
using Level;
using UnityEngine;

[RequireComponent(typeof(CharacterSkin), typeof(CharacterAnimator))]
public class PlayerInfo : MonoBehaviour
{
    public string Nickname { get; private set; }

    public Color Color { get; private set; }
    public Crowd Crowd;

    private CharacterSkin _skin;
    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        Crowd = GetComponentInChildren<Crowd>();
        
        _skin = GetComponent<CharacterSkin>();
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void Start()
    {
        LevelManager.Instance.OnStartGame += () =>
            _characterAnimator.ChangeAnimationState(CharacterAnimator.ECharacterAnimationState.Run);
        
        LevelManager.Instance.OnTimeOut += () =>
            _characterAnimator.ChangeAnimationState(CharacterAnimator.ECharacterAnimationState.Idle);
    }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
    }

    public void SetColor(Color color)
    {
        Color = color;
        _skin.SetColor(color);
    }
}
