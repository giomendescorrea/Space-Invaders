using UnityEngine;

public class Missile : MonoBehaviour
{
    public enum Dono { Jogador, Inimigo }

    public Dono dono = Dono.Jogador;
    public float speed = 8f;

    public AudioSource source;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        float direcaoY = (dono == Dono.Jogador) ? 1f : -1f;
        rb2d.linearVelocity = new Vector2(0, direcaoY * speed);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (dono == Dono.Jogador && coll.collider.CompareTag("Alien"))
        {
            if (source != null) source.Play();

            Alien alien = coll.collider.GetComponent<Alien>();
            if (alien != null)
            {
                alien.Destruir();
            }

            Destroy(gameObject);
        }
        else if (dono == Dono.Jogador && coll.collider.CompareTag("Boss"))
        {
            if (source != null) source.Play();

            BossShip boss = coll.collider.GetComponent<BossShip>();
            if (boss != null)
            {
                boss.Destruir();
            }

            Destroy(gameObject);
        }
        else if (dono == Dono.Inimigo && coll.collider.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.PerderVida();
            }

            Destroy(gameObject);
        }
        else if (coll.collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
