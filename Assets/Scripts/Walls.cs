using UnityEngine;

public class Walls : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Missile"))
        {
            Destroy(coll.gameObject);
        }
    }
}
