using UnityEngine;

public static class EnemyAnimatornData
{
    public static class Params
    {
        public static readonly int Hurt = Animator.StringToHash(nameof(Hurt));
        public static readonly int IsDead = Animator.StringToHash(nameof(IsDead));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int IsStay = Animator.StringToHash(nameof(IsStay));
    }
}

[RequireComponent(typeof(Health), typeof(EnemyPatrolGround), typeof(EnemyCombat))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animatorEnemy;
    private Health _enemyHealth;
    private EnemyPatrolGround _enemyPatrolGround;
    private EnemyCombat _enemyCombat;

    private void Awake()
    {
        _animatorEnemy = GetComponent<Animator>();
        _enemyHealth = GetComponent<Health>();
        _enemyPatrolGround = GetComponent<EnemyPatrolGround>();
        _enemyCombat = GetComponent<EnemyCombat>();
    }

    private void OnEnable()
    {
        _enemyHealth.Died += OnAnimationDeath;
        _enemyHealth.Hurt += OnHurtAnimation;
        _enemyPatrolGround.Stayed += OnAnimationStay;
        _enemyPatrolGround.Run += OnAnimationRun;
        _enemyCombat.Attacked += OnAnimationAttack;
    }

    private void OnDisable()
    {
        _enemyHealth.Died -= OnAnimationDeath;
        _enemyHealth.Hurt -= OnHurtAnimation;
        _enemyPatrolGround.Stayed -= OnAnimationStay;
        _enemyPatrolGround.Run -= OnAnimationRun;
        _enemyCombat.Attacked -= OnAnimationAttack;
    }

    private void OnHurtAnimation()
    {
        _animatorEnemy.SetTrigger(EnemyAnimatornData.Params.Hurt);
    }

    private void OnAnimationDeath()
    {
        _animatorEnemy.SetBool(EnemyAnimatornData.Params.IsDead, true);
    }

    private void OnAnimationAttack()
    {
        _animatorEnemy.SetTrigger(EnemyAnimatornData.Params.Attack);
    }

    private void OnAnimationStay()
    {
        _animatorEnemy.SetBool(EnemyAnimatornData.Params.IsStay, true);
    }

    private void OnAnimationRun()
    {
        _animatorEnemy?.SetBool(EnemyAnimatornData.Params.IsStay, false);
    }
}