using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("=== Spawn Setting")]
    public GameObject enemyPrefab;

    [Header("=== Spawn Timer")]
    public float timeLimit = 1.0f;
    public float timer = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeLimit)
        {
            Vector3 randomPosition = transform.position;
            randomPosition.x += Random.Range(-9.0f, 9.0f);

            GameObject clone = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            timer = 0.0f;
        }
    }
}
