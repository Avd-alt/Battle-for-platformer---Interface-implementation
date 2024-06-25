using UnityEngine;

public static class PlayerAnimatornData
{
    public static class Params
    {
        public static readonly int Run = Animator.StringToHash(nameof(Run));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int Hurt = Animator.StringToHash(nameof(Hurt));
        public static readonly int IsDead = Animator.StringToHash(nameof(IsDead));
    }
}

[RequireComponent(typeof(Health), typeof(PlayerMover), typeof(PlayerCombat))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animatorPlayer;
    private Health _health;
    private PlayerMover _playerMover;
    private PlayerCombat _playerCombat;

    private void Awake()
    {
        _animatorPlayer = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _playerMover = GetComponent<PlayerMover>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        _health.Hurt += OnAnimationHurt;
        _health.Died += OnDeathAnimation;
        _playerMover.Run += OnRunAnimation;
        _playerMover.StoppedRun += OffRunAnimation;
        _playerMover.Jumped += OnJumpAnimation;
        _playerCombat.Attacked += AttackAnimation;
    }

    private void OnDisable()
    {
        _health.Hurt -= OnAnimationHurt;
        _health.Died -= OnDeathAnimation;
        _playerMover.Run -= OnRunAnimation;
        _playerMover.StoppedRun -= OffRunAnimation;
        _playerMover.Jumped -= OnJumpAnimation;
        _playerCombat.Attacked -= AttackAnimation;
    }

    private void OnRunAnimation()
    {
        _animatorPlayer.SetBool(PlayerAnimatornData.Params.Run, true);
    }

    private void OffRunAnimation()
    {
        _animatorPlayer.SetBool(PlayerAnimatornData.Params.Run, false);
    }

    private void OnJumpAnimation()
    {
        _animatorPlayer.SetTrigger(PlayerAnimatornData.Params.Jump);
    }

    private void AttackAnimation()
    {
        _animatorPlayer.SetTrigger(PlayerAnimatornData.Params.Attack);
    }

    private void OnAnimationHurt()
    {
        _animatorPlayer.SetTrigger(PlayerAnimatornData.Params.Hurt);
    }

    private void OnDeathAnimation()
    {
        _animatorPlayer.SetBool(PlayerAnimatornData.Params.IsDead, true);
    }
}