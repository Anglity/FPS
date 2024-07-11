using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float maxDistance = 1000000;  // Máxima distancia a la que la bala puede detectar colisiones
    public GameObject decalHitWall;      // Prefab para el impacto en paredes
    public GameObject bloodEffect;       // Prefab para el efecto de sangre cuando golpea a un Dummie
    public GameObject sparkEffect;       // Prefab para el efecto de chispas cuando golpea un obstáculo
    public LayerMask ignoreLayer;        // Capas que el raycast debe ignorar
    public AudioClip impactSound;        // Sonido de impacto
    public AudioSource audioSource;      // Fuente de audio para reproducir sonidos

    private static Dictionary<GameObject, int> hitCounts = new Dictionary<GameObject, int>(); // Diccionario para llevar cuenta de los impactos por objeto

    void Update()
    {
        RaycastHit hit;
        // Realiza un raycast desde la posición de la bala hacia adelante hasta una distancia máxima
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayer))
        {
            HandleImpact(hit);  // Manejar el impacto detectado
            Destroy(gameObject); // Destruir la bala después del impacto
        }
        else
        {
            Destroy(gameObject, 0.1f); // Asegurar la destrucción de la bala si no impacta nada
        }
    }

    // Gestiona el impacto dependiendo del tipo de objeto golpeado
    void HandleImpact(RaycastHit hit)
    {
        if (hit.transform.CompareTag("LevelPart"))
        {
            Instantiate(decalHitWall, hit.point + hit.normal * 0.1f, Quaternion.LookRotation(hit.normal));
        }
        else if (hit.transform.CompareTag("Dummie"))
        {
            Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            HandleHit(hit.transform.gameObject);
        }
        else if (hit.transform.CompareTag("Obstacle"))
        {
            Instantiate(sparkEffect, hit.point, Quaternion.LookRotation(hit.normal));
            PlayImpactSound();
        }
    }

    // Maneja los golpes a los dummies y a los ghouls
    void HandleHit(GameObject target)
    {
        if (target.GetComponent<Ghoul>() != null)
        {
            Ghoul ghoul = target.GetComponent<Ghoul>();
            ghoul.TakeDamage(1); // Aplica 1 de daño al Ghoul
        }
        else
        {
            int requiredHits = 3; // Número de golpes necesarios para un dummie normal

            if (!hitCounts.ContainsKey(target))
            {
                hitCounts.Add(target, 0);
            }
            hitCounts[target]++;
            if (hitCounts[target] >= requiredHits) // Si el objetivo ha sido golpeado el número requerido de veces
            {
                GameManager.Instance.IncreaseScore(1); // Aumenta la puntuación en 1
                Destroy(target); // Destruye el Dummie
                hitCounts.Remove(target); // Remueve el objetivo del diccionario
            }
        }
    }

    // Reproduce un sonido de impacto
    void PlayImpactSound()
    {
        if (impactSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
    }
}
