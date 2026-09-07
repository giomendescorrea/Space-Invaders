using UnityEngine;

public class BossShip : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 3f;
    public float rightLimitX = 5f;

    void Update()
    {
        if (!GameManager.isGameStarted) return;

        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x >= rightLimitX)
        {
            Destroy(gameObject);
        }
    }

    public void Destruir()
    {
        GameManager.Score("Chefe");
        Destroy(gameObject);
    }
}
