using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float health = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ƒ_ƒ[ƒW‚ğó‚¯‚½‚Æ‚«‚ÉŒÄ‚Î‚ê‚éŠÖ”
    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    // €–S‚µ‚½‚Æ‚«‚ÉŒÄ‚Î‚ê‚éŠÖ”
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
    

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
