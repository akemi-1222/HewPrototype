using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float keyMoveSpeed = 4.0f;  // キー押されている時のスピード

    // Lスティック操作時の速度倍率
    [SerializeField] private float stickSpeedRate = 1.3f;

    [Header("ジャンプ力")]
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float smallJumpPower = 2.5f;
    [SerializeField] private float jumpGravity = 15.0f;
    [SerializeField] private float fallPower = 25.0f;

    // キャラクターモデルの向きの補正
    [SerializeField] private float playerAngleOffset = 90.0f;

    [SerializeField] private Transform player;

    [SerializeField] private Transform cameraTransform;

    Rigidbody rb;

    Vector3 moveDirection;

    float f_currentSpeed;

    bool b_jump;
    bool b_smallJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        f_currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
{
    Vector2 moveInput = Vector2.zero;
    bool usingStick = false;

    // キーボード入力
    
        Keyboard keyboard = Keyboard.current;

        moveInput.x =
            (keyboard.dKey.isPressed ? 1f : 0f) -
            (keyboard.aKey.isPressed ? 1f : 0f);

        moveInput.y =
            (keyboard.wKey.isPressed ? 1f : 0f) -
            (keyboard.sKey.isPressed ? 1f : 0f);

        if (keyboard.spaceKey.wasPressedThisFrame)
            b_jump = true;

        if (keyboard.jKey.wasPressedThisFrame)
            b_smallJump = true;
    

    // コントローラー入力
    
        Gamepad gamepad = Gamepad.current;
        Vector2 stick = gamepad.leftStick.ReadValue();

        if (stick.magnitude > 0.1f)
        {
            moveInput = stick;
            usingStick = true;
        }

        // 右側ボタン：通常ジャンプ
        if (gamepad.buttonEast.wasPressedThisFrame)
            b_jump = true;

        // 下側ボタン：小ジャンプ
        if (gamepad.buttonSouth.wasPressedThisFrame)
            b_smallJump = true;
    

    // 斜め入力でも速度が大きくなりすぎないようにする
    moveInput = Vector2.ClampMagnitude(moveInput, 1f);

    // カメラの前方を地面に沿った方向へ変換する
    Vector3 forward = Vector3.forward;

    if (cameraTransform != null)
    {
        forward = Vector3.ProjectOnPlane(
            cameraTransform.forward,
            Vector3.up
        );

        // カメラがほぼ真下を向いている場合の補助
        if (forward.sqrMagnitude < 0.001f)
        {
            forward = Vector3.ProjectOnPlane(
                cameraTransform.up,
                Vector3.up
            );
        }

        forward.Normalize();
    }

    Vector3 right = Vector3.Cross(Vector3.up, forward);

        if (moveInput.sqrMagnitude > 0.01f)
        {
            moveDirection = (
                forward * moveInput.y +
                right * moveInput.x
            ).normalized;

            f_currentSpeed = usingStick
                ? moveSpeed * stickSpeedRate
                : keyMoveSpeed;
        }

        // 入力があるときだけ、移動先へ向きを変える
        if (player != null && moveDirection.sqrMagnitude > 0.001f)
    {
        Quaternion targetRotation =
            Quaternion.LookRotation(
                moveDirection.normalized,
                Vector3.up
            ) *
            Quaternion.Euler(0f, playerAngleOffset, 0f);

        player.rotation = Quaternion.RotateTowards(
            player.rotation,
            targetRotation,
            720f * Time.deltaTime
        );
    }
}

    void FixedUpdate()
    {
        // 移動
        rb.MovePosition(rb.position + moveDirection * f_currentSpeed * Time.fixedDeltaTime);

        // ジャンプ
        if (b_jump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            b_jump = false;
        }

        // 小ジャンプ
        if (b_smallJump)
        {
            rb.AddForce(Vector3.up * smallJumpPower, ForceMode.Impulse);

            b_smallJump = false;
        }

        // 上昇中
        if (rb.linearVelocity.y > 0)
        {
            rb.AddForce(Vector3.down * jumpGravity, ForceMode.Acceleration);
        }

        // 落下中
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.down * fallPower, ForceMode.Acceleration);
        }
    }
}
