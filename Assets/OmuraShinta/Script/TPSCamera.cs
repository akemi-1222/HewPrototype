using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCamera : MonoBehaviour
{
    [Header("追従対象")]
    [SerializeField] private Transform target;

    [Header("カメラ設定")]
    [SerializeField] private float cameraDistance = 5.0f; // カメラと対象の距離
    [SerializeField] private float cameraHeight = 2.0f; // カメラの高さ
    [SerializeField] private float mouthSensitivityX = 0.75f; // 縦のマウス感度
    [SerializeField] private float mouthSensitivityY = 0.15f; // 横のマウス感度
    [SerializeField] private float controllerSensitivityX = 0.15f; // 縦のコントローラー感度
    [SerializeField] private float controllerSensitivityY = 0.3f; // 横のコントローラー感度

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

        //マウスと右スティックの入力を取得
        Vector2 cameraInput = Vector2.zero;

        //マウスの入力を取得
        if(Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            cameraInput.x += mouseDelta.x * mouthSensitivityX;
            cameraInput.y += mouseDelta.y * mouthSensitivityY;

        }

        //コントローラーの右スティックの入力を取得
        if (Gamepad.current != null)
        {
            Vector2 stickInput = Gamepad.current.rightStick.ReadValue();

           cameraInput.x += stickInput.x * mouthSensitivityX;
           cameraInput.y += stickInput.y * mouthSensitivityY;
        }

        //マウスの左右移動でのカメラを横回転
        m_rotationX += cameraInput.x;

        //マウスの上下移動でのカメラを縦回転
        m_rotationY -= cameraInput.y;

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
