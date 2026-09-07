using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Vector3 spawnPosition = new Vector3(-6f, 4f, 0f);

    public float minInterval = 1f;
    public float maxInterval = 3f;

    void Start()
    {
        AgendarProximoBoss();
    }

    private void AgendarProximoBoss()
    {
        float tempo = Random.Range(minInterval, maxInterval);
        Invoke(nameof(SpawnBoss), tempo);
    }

    private void SpawnBoss()
    {
        if (GameManager.isGameStarted && bossPrefab != null)
        {
            GameObject bossObj = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            bossObj.SetActive(true); // Necessário caso o molde na Hierarchy esteja desativado
        }

        AgendarProximoBoss();
    }
}
