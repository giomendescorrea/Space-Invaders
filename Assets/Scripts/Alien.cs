using UnityEngine;

public class Alien : MonoBehaviour
{
    public enum TipoAlien { Tipo1, Tipo2, Tipo3 }

    public TipoAlien tipo = TipoAlien.Tipo1;

    private AlienFormation formation;

    void Start()
    {
        formation = GetComponentInParent<AlienFormation>();
    }

    public void Destruir()
    {
        GameManager.Score(tipo.ToString());

        if (formation != null)
        {
            formation.RemoverAlienDaContagem();
        }
        else if (GameManager.instance != null)
        {
            GameManager.instance.AlienDestruido();
        }

        Destroy(gameObject);
    }
}
