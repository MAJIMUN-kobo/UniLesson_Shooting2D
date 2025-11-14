using UnityEngine;

/// <summary>
/// 弾丸の基本クラス
/// </summary>
public class BaseBullet : MonoBehaviour
{
    [Header(">> Base Settings")]
    public GameObject owner;    // 弾丸の発射者
    public float shotPower;     // 弾丸の発射威力
    public float attack;        // 弾丸の攻撃力
    public float lifeTime;      // 弾丸の寿命(秒)
    public Vector3 direction;   // 弾丸の進行方向(ベクトル)
    public Transform target;    // 弾丸のターゲット

    protected Vector2 velocity; // 弾丸の速度ベクトル
    protected float lifeTimer;  // 弾丸の寿命タイマー

    // 弾丸の状態遷移番号
    public int statePattern { get; protected set; }

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    /// <param name="owner">管理者</param>
    /// <param name="shotPower">発射威力</param>
    /// <param name="attack">攻撃力</param>
    /// <param name="lifeTime">生存時間</param>
    /// <param name="direction">向き</param>
    /// <param name="target">ターゲット</param>
    public void Initialize( GameObject owner, float shotPower, float attack, float lifeTime, Vector3 direction, Transform target)
    {
        this.owner = owner;
        this.shotPower = shotPower;
        this.attack = attack;
        this.lifeTime = lifeTime;
        this.direction = direction;
        this.target = target;
    }
}
