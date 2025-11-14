using UnityEngine;

public class Enemy : BaseEnemy
{
    public GameObject damageParticle;  // ダメージエフェクト

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    /// <summary>
    /// Collider同士が"接触した瞬間"に実行される関数
    /// </summary>
    /// <param name="collision"> 接触したオブジェクトの情報(Collision2D) </param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($" HIT!! ... { collision.transform.name } ");
        //Destroy( gameObject );              // 接触で削除される
        //Destroy( collision.gameObject );    // 接触したオブジェクトも削除する
    }

    /// <summary>
    /// Collider同士が"接触している間"実行される関数
    /// </summary>
    void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    /// <summary>
    /// Collider同士が"離れた瞬間"実行される関数
    /// </summary>
    void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // 弾丸(タグが Bullet のオブジェクト)に当たったら消える
        if (collision.transform.tag.Contains("Bullet"))
        {
            OnDamage(10);                   // 自身にダメージ
            Destroy(collision.gameObject);  // 弾丸を消す

            // ダメージエフェクトを生成
            GameObject effect = Instantiate(damageParticle, transform.position, transform.rotation);
            Destroy( effect, 5.0f );
        }

        base.OnTriggerEnter2D(collision);
    }
}
