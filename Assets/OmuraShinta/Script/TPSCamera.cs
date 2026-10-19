using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCamera : MonoBehaviour
{
    [Header("追従対象")]
    [SerializeField] private Transform target;

    [Header("カメラ設定")]
    [SerializeField] private float cameraDistance = 5.0f; // カメラと対象の距離
    [SerializeField] private float cameraHeight = 2.0f; // カメラの高さ
    [SerializeField] private float mouthSensitivity = 0.15f; // マウス感度

    Vector3 m_angles;

    float m_rotationX;
    float m_rotationY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //カメラの初期角度を取得
        m_angles = transform.eulerAngles;

        //カメラの初期角度を保存
        m_rotationX = m_angles.x;
        m_rotationY = m_angles.y;

        //マウスカーソルを画面中央に固定&非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //マウスの移動量を取得
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        //マウスの左右移動でのカメラを横回転
        m_rotationX += mouseDelta.x * mouthSensitivity;

        //マウスの上下移動でのカメラを縦回転
        m_rotationY -= mouseDelta.y * mouthSensitivity;

        //カメラの縦回転の角度制限
        m_rotationY = Mathf.Clamp(m_rotationY, -30f, 60f);

        //カメラの向きを計算
        Quaternion rotation =
            Quaternion.Euler(
                m_rotationY,
                m_rotationX,
                0);


        //追従対象の位置を取得
        Vector3 targetPosition = target.position;

        // カメラの後ろ側への移動量を計算
        Vector3 offset = rotation * new Vector3(0, 0, -cameraDistance);

        // カメラの位置を設定
        transform.position = targetPosition
            + Vector3.up * cameraHeight
            + offset;

        // 追従対象の方向を向く
        transform.LookAt(targetPosition + Vector3.up * cameraHeight);

    }
}
