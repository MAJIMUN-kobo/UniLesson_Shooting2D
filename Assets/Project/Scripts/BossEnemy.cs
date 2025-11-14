using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public enum BossState
    {
        Appear, // 出現
        Idle,   // 待機
        Attack_01, // 攻撃1 : ばらまき射撃
        Attack_02, // 攻撃2 : 集中射撃
        Dead       // 死亡
    }

    public BossState currentState;
    public Vector3 defaultPosition = new Vector3(0, 2.0f, 0);
    public float attackInterval = 2.0f;
    public float attackTimer    = 0.0f;
    public float shotInterval   = 2.0f;
    public float shotTimer      = 0.0f;

    public GameObject bulletPrefab;
    public float shotPower = 10.0f;

    void Start()
    {
        currentState = BossState.Appear;
    }

    void Update()
    {
        switch( currentState )
        {
            case BossState.Appear:
                Appear();
                break;

            case BossState.Idle:
                Idle();
                break;

            case BossState.Attack_01:
                Attack_01();
                break;

            case BossState.Attack_02:
                Attack_02();
                break;

            case BossState.Dead:
                Dead();
                break;
        }
    }

    // 出現処理
    public void Appear()
    {
        transform.position = Vector3.Lerp(
            transform.position,     // 現在の座標を
            defaultPosition,        // 目的の座標へ
            Time.deltaTime );

        float distance = Vector3.Distance(
            transform.position, // 座標A 
            defaultPosition );  // 座標B

        if(distance <= 0.01f)
        {   // 目的地に到達したら待機状態へ移行
            currentState = BossState.Idle;
        }
    }

    // 待機処理
    public void Idle()
    {
        attackTimer += Time.deltaTime;
        if( attackTimer >= attackInterval )
        {
            attackTimer = 0.0f;
            attackInterval = Random.Range(1.0f, 2.0f);

            int random = Random.Range(0, 100);
            if(random > 50)
                currentState = BossState.Attack_01;
            else
                currentState = BossState.Attack_02;
        }
    }

    // 攻撃1 : ばらまき射撃
    public void Attack_01()
    {
        Shot( new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) );

        shotTimer += Time.deltaTime;
        if( shotTimer >= shotInterval )
        {
            shotTimer = 0.0f;
            currentState = BossState.Idle;
        }
    }

    // 攻撃2 : 集中射撃
    public void Attack_02()
    {
        Shot( new Vector2(0, -1) );

        shotTimer += Time.deltaTime;
        if (shotTimer >= shotInterval)
        {
            shotTimer = 0.0f;
            currentState = BossState.Idle;
        }
    }

    // 死亡処理
    public void Dead()
    {
        
    }

    // 射撃
    public void Shot( Vector2 dir )
    {
        // 弾丸を生成
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Destroy(bullet, 3.0f);  // n秒後に弾を削除

        // 弾丸の初期化
        BaseBullet baseBullet = bullet.GetComponent<BaseBullet>();
        if(baseBullet != null)
        {
            baseBullet.Initialize(
                owner     : gameObject,
                shotPower : shotPower,
                attack    : 10.0f,
                lifeTime  : 3.0f,
                direction : dir,
                target    : null );
        }


        // 射撃方向を決める
        Vector2 direction = dir;

        // Rigidbody2Dを取得して力を加える
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        rb2d.AddForce(direction * shotPower * Random.Range(0.6f, 1f), ForceMode2D.Impulse);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);

        // 弾丸(タグが Bullet のオブジェクト)に当たったら消える
        if (collision.transform.tag.Contains("Bullet"))
        {
            OnDamage(10);
            ///Destroy(collision.gameObject);
        }

        base.OnTriggerEnter2D(collision);
    }

    public override void OnDamage(float damage)
    {
        if (damage < 100) damage = 0; // 100未満のダメージは無効
        base.OnDamage(damage);
    }
}
