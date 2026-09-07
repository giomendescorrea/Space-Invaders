using UnityEngine;

public class AlienFormation : MonoBehaviour
{
    [Header("Movimento da Horda")]
    public float speed = 1.0f;
    public float xBound = 4.0f;
    public float dropDistance = 0.5f;
    public float bottomLimitY = -3.5f; 

    [Header("Tiro dos Aliens")]
    public GameObject enemyMissilePrefab;
    public float minFireInterval = 1.0f;
    public float maxFireInterval = 3.0f;

    private int direction = 1;
    private float fireTimer;

    void Start()
    {
        SetNextFireTime();
    }

    void Update()
    {
        if (!GameManager.isGameStarted) return;

        MoverHorda();
        AtualizarTiro();
    }

    private void MoverHorda()
    {
        Vector3 pos = transform.position;
        pos.x += direction * speed * Time.deltaTime;

        if (pos.x >= xBound || pos.x <= -xBound)
        {
            direction *= -1;
            pos.x = Mathf.Clamp(pos.x, -xBound, xBound);
            pos.y -= dropDistance;
        }

        transform.position = pos;

        VerificarInvasao();
    }

    private void VerificarInvasao()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform alien = transform.GetChild(i);

            if (alien.gameObject.activeInHierarchy && alien.position.y <= bottomLimitY)
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.PerderJogoPorInvasao();
                }
                return;
            }
        }
    }

    private void AtualizarTiro()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            TentarAtirar();
            SetNextFireTime();
        }
    }

    private void SetNextFireTime()
    {
        fireTimer = Random.Range(minFireInterval, maxFireInterval);
    }

    private void TentarAtirar()
    {
        if (transform.childCount == 0 || enemyMissilePrefab == null) return;

        const int tentativas = 10;
        for (int i = 0; i < tentativas; i++)
        {
            int idx = Random.Range(0, transform.childCount);
            Transform alienEscolhido = transform.GetChild(idx);

            if (alienEscolhido != null && alienEscolhido.gameObject.activeInHierarchy)
            {
                GameObject missileObj = Instantiate(enemyMissilePrefab, alienEscolhido.position, Quaternion.identity);
                missileObj.SetActive(true); // Necessário caso o molde na Hierarchy esteja desativado
                Missile missile = missileObj.GetComponent<Missile>();
                if (missile != null)
                {
                    missile.dono = Missile.Dono.Inimigo;
                }
                break;
            }
        }
    }

    public void RemoverAlienDaContagem()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AlienDestruido();
        }
    }
}
