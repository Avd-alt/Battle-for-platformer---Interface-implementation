using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Speed = nameof(Speed);

    public float HorizontalDirection { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsTryingJump { get; private set; }
    public bool IsTryingAttack { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            IsTryingJump = true;

        if (Input.GetMouseButton(0))
            IsTryingAttack = true;

        IsMoving = Input.GetButton(Horizontal) ? true : false;

        HorizontalDirection = Input.GetAxis(Horizontal);
    }

    public void DeActivateJumpTrying()
    {
        IsTryingJump = false;
    }

    public void DeActivateAttackTrying()
    {
        IsTryingAttack = false;
    }
}
