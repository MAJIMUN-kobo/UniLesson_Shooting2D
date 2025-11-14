using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("=== Base Parameter")]
    public float moveSpeed = 5.0f;

    [Header("=== Bullet Setting")]
    public GameObject bulletPrefab;
    public float shotPower = 10f;
    public float shotLine = 2;
    public float shotInterval = 0.5f;       // 発射間隔
    private float _shotIntervalTimer = 0f;  // 発射間隔タイマー

    [Header("=== Input Setting")]
    public InputActionReference moveInput;
    public InputActionReference fireInput;
    public PlayerInput playerInput;

    private Vector2 _moveInputValue;
    private float   _fireInputValue;

    [HideInInspector] public Rigidbody2D bulletRb;

    void Start()
    {
        // インプットアクションイベントの登録
        moveInput.action.performed += MoveInputActionPerformed;
        moveInput.action.canceled += MoveInputActionCanceled;
        fireInput.action.performed += FireInputActionPerformed;
        fireInput.action.canceled += FireInputActionCanceled;
        moveInput.action.Enable();
        fireInput.action.Enable();
    }

    /// <summary>
    /// インプットアクションイベント：移動
    /// </summary>
    /// <param name="context"></param>
    private void MoveInputActionPerformed(InputAction.CallbackContext context)
    {
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// インプットアクションイベント：移動キャンセル
    /// </summary>
    /// <param name="context"></param>
    private void MoveInputActionCanceled(InputAction.CallbackContext context)
    {
        _moveInputValue = Vector2.zero;
    }

    private void FireInputActionPerformed(InputAction.CallbackContext context)
    {
        _fireInputValue = context.ReadValue<float>();
    }

    private void FireInputActionCanceled(InputAction.CallbackContext context)
    {
        _fireInputValue = 0f;
    }

    void Update()
    {
        // 移動処理
        transform.Translate(_moveInputValue.x * moveSpeed * Time.deltaTime, _moveInputValue.y * moveSpeed * Time.deltaTime, 0);

        // 発射間隔タイマー更新
        _shotIntervalTimer += Time.deltaTime;
        if ( _fireInputValue >= 0.5f )
        {
            // 発射間隔を過ぎていたら弾を発射
            if (_shotIntervalTimer >= shotInterval)
            {
                for (int i = 0; i < shotLine; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(i - 0.5f, 0, 0), Quaternion.identity);
                    bulletRb = bullet.GetComponent<Rigidbody2D>();
                    Destroy(bullet, 1f);    // n秒後に弾を消す

                    // AddFoce（ 飛ばす方向と力, 飛ばすときのモード変更 ）
                    bulletRb.AddForce(new Vector2(0, 1) * shotPower, ForceMode2D.Impulse);

                    // 発射間隔タイマーリセット
                    _shotIntervalTimer = 0f;
                }
            }
        }
    }
}
