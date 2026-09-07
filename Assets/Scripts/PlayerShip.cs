using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [Header("Controles")]
    public KeyCode moveLeft = KeyCode.LeftArrow;
    public KeyCode moveRight = KeyCode.RightArrow;
    public KeyCode shoot = KeyCode.Space;

    [Header("Movimento")]
    public float speed = 6.0f;
    public float boundX = 4.0f; 

    [Header("Tiro")]
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;
    public float fireCooldown = 0.5f;   

    private Rigidbody2D rb2d;
    public AudioSource source;

    private float cooldownTimer = 0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.isGameStarted) return;

        var vel = rb2d.linearVelocity;
        if (Input.GetKey(moveLeft))
        {
            vel.x = -speed;
        }
        else if (Input.GetKey(moveRight))
        {
            vel.x = speed;
        }
        else
        {
            vel.x = 0;
        }
        rb2d.linearVelocity = vel;

        var pos = transform.position;
        if (pos.x > boundX)
        {
            pos.x = boundX;
        }
        else if (pos.x < -boundX)
        {
            pos.x = -boundX;
        }
        transform.position = pos;

        cooldownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(shoot) && cooldownTimer <= 0f)
        {
            Atirar();
            cooldownTimer = fireCooldown;
        }
    }

    void Atirar()
    {
        if (missilePrefab == null) return;

        Vector3 spawnPos = missileSpawnPoint != null ? missileSpawnPoint.position : transform.position;
        GameObject missileObj = Instantiate(missilePrefab, spawnPos, Quaternion.identity);
        missileObj.SetActive(true); // Necessário caso o molde na Hierarchy esteja desativado

        Missile missile = missileObj.GetComponent<Missile>();
        if (missile != null)
        {
            missile.dono = Missile.Dono.Jogador;
        }

        if (source != null)
        {
            source.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (source != null)
        {
            source.Play();
        }
    }
}
