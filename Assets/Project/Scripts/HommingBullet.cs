using UnityEngine;

/// <summary>
/// 追尾弾クラス
/// </summary>
public class HommingBullet : BaseBullet
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = SearchTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Movement()
    {
        // ターゲットがいなければ処理しない
        if (target == null)
        {
            Debug.LogWarning($"{transform.name}: target not found.");
            return;
        }

        // ターゲットの方向ベクトルを計算
        direction = target.position - transform.position;

        // ベクトルを"正規化"して速度ベクトルを計算
        velocity = direction.normalized * shotPower;

        // 移動させる
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// ターゲットを探すメソッド
    /// </summary>
    /// <returns>ターゲットの Transform </returns>
    private Transform SearchTarget()
    {
        return GameObject.Find("Player").transform;
    }
}
