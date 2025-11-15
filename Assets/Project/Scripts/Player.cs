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

    void OnEnable()
    {
        RegisterInputActions();
    }

    void OnDisable()
    {
        
    }


    void Start()
    {
    }


    /// <summary>
    /// アクションイベント：移動
    /// </summary>
    /// <param name="context">アクションコールバック</param>
    private void MoveInputActionPerformed(InputAction.CallbackContext context)
    {
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// アクションイベント：移動のキャンセル
    /// </summary>
    /// <param name="context">アクションコールバック</param>
    private void MoveInputActionCanceled(InputAction.CallbackContext context)
    {
        _moveInputValue = Vector2.zero;
    }

    /// <summary>
    /// アクションイベント：射撃
    /// </summary>
    /// <param name="context">アクションコールバック</param>
    private void FireInputActionPerformed(InputAction.CallbackContext context)
    {
        _fireInputValue = context.ReadValue<float>();
    }

    /// <summary>
    /// アクションイベント：射撃のキャンセル
    /// </summary>
    /// <param name="context">アクションコールバック</param>
    private void FireInputActionCanceled(InputAction.CallbackContext context)
    {
        _fireInputValue = 0f;
    }

    /// <summary>
    /// アクションイベントの登録
    /// </summary>
    private void RegisterInputActions()
    {
        moveInput.action.performed += MoveInputActionPerformed;
        moveInput.action.canceled  += MoveInputActionCanceled;
        fireInput.action.performed += FireInputActionPerformed;
        fireInput.action.canceled  += FireInputActionCanceled;

        moveInput.action.Enable();
        fireInput.action.Enable();
    }

    /// <summary>
    /// アクションイベントの解放
    /// </summary>
    private void ReleaseInputActions()
    {
        moveInput.action.performed -= MoveInputActionPerformed;
        moveInput.action.canceled  -= MoveInputActionCanceled;
        fireInput.action.performed -= FireInputActionPerformed;
        fireInput.action.canceled  -= FireInputActionCanceled;

        moveInput.action.Disable();
        fireInput.action.Disable();
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
                    Vector3 shotPosition = transform.position + new Vector3(i - 0.5f, 0, 0);
                    Quaternion shotRotation = Quaternion.identity;
                    Vector3 shotForce = new Vector3(0, 1) * shotPower;

                    Shot(bulletPrefab, shotPosition, shotRotation, shotForce, 1f);
                }
            }
        }
    }

    /// <summary>
    /// 弾丸の発射
    /// </summary>
    /// <param name="original">弾丸のプレハブ</param>
    /// <param name="position">発射座標</param>
    /// <param name="rotation">発射角度</param>
    /// <param name="direction">進行方向</param>
    /// <param name="lifeTime">生存時間</param>
    private void Shot(GameObject original, Vector3 position, Quaternion rotation, Vector3 direction, float lifeTime)
    {
        GameObject clone = Instantiate(original, position, rotation);
        BaseBullet bullet = clone.GetComponent<BaseBullet>();
        bullet.Initialize(gameObject, shotPower, 1, lifeTime, direction, null);

        Destroy(bullet, lifeTime);
    }
}
