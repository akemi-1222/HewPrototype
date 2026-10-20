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

    [SerializeField] private Transform player;

    // キャラクターモデルの向きの補正
    [SerializeField] private float playerAngleOffset = 90.0f;

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
        // キーボード
        if (Keyboard.current != null)
        {
            // 最後に押したキーの方向を保存
            if (Keyboard.current.wKey.wasPressedThisFrame)
                moveDirection = Vector3.forward;

            if (Keyboard.current.sKey.wasPressedThisFrame)
                moveDirection = Vector3.back;

            if (Keyboard.current.dKey.wasPressedThisFrame)
                moveDirection = Vector3.right;

            if (Keyboard.current.aKey.wasPressedThisFrame)
                moveDirection = Vector3.left;


            // 移動キーを押している間は速度を変更
            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.sKey.isPressed ||
                Keyboard.current.dKey.isPressed ||
                Keyboard.current.aKey.isPressed)
            {
                f_currentSpeed = keyMoveSpeed;
            }
            else
            {
                f_currentSpeed = moveSpeed;
            }


            // Spaceでジャンプ
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                b_jump = true;

            // Jで小ジャンプ
            if (Keyboard.current.jKey.wasPressedThisFrame)
                b_smallJump = true;
        }


        // コントローラー
        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();

            // Lスティックを倒している
            if (stick.magnitude > 0.1f)
            {
                // スティックの方向を3Dの方向に変換
                moveDirection = new Vector3(stick.x, 0, stick.y).normalized;

                // 通常速度の1.3倍
                f_currentSpeed = moveSpeed * stickSpeedRate;
            }


            // ○ボタン
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                // ジャンプ
                b_jump = true;

                // バクつく
                b_smallJump = true;
            }


            // ×ボタン
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                // バクつく
                b_smallJump = true;
            }
        }


        // キャラクターの向き

        if (moveDirection != Vector3.zero)
        {
            player.forward = moveDirection;
            player.Rotate(0, playerAngleOffset, 0);
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
