using UnityEngine;

public class Ghoul : MonoBehaviour
{
    public int maxHits = 10; // Número máximo de impactos que el Ghoul puede recibir
    private int currentHits = 0; // Número actual de impactos recibidos

    public void TakeDamage(int damage)
    {
        currentHits += damage;
        if (currentHits >= maxHits)
        {
            DestroyGhoul();
        }
    }

    private void DestroyGhoul()
    {
        GameManager.Instance.IncreaseScore(1); // Aumenta la puntuación en 1 al destruir el Ghoul
        Destroy(gameObject); // Destruye el Ghoul
    }
}
