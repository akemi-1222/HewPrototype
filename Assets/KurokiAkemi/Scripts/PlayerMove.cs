using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;

    [SerializeField] private Transform player;

    // キャラクターモデルの向きの補正
    [SerializeField] private float playerAngleOffset = 90.0f;

    Rigidbody rb;

    Vector3 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            moveDirection = Vector3.forward;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            moveDirection = Vector3.back;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            moveDirection = Vector3.right;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            moveDirection = Vector3.left;
        }

        // キャラクターを移動方向に向かせる
        if (moveDirection != Vector3.zero)
        {
            player.forward = moveDirection;
            player.Rotate(0, playerAngleOffset, 0);
        }

        // 移動
        rb.MovePosition(
            rb.position + moveDirection * moveSpeed * Time.deltaTime
        );

    }
}
