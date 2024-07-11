using UnityEngine;

public class ObstacleImpact : MonoBehaviour
{
    [Tooltip("Sonido que se reproducirá al impactar con el obstáculo")]
    public AudioClip impactSound;

    private AudioSource audioSource;

    void Start()
    {
        // Asegúrate de que hay un AudioSource componente y configúralo
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    public void PlayImpactSound()
    {
        if (impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
        else
        {
            Debug.LogWarning("No impact sound has been set for the obstacle.");
        }
    }
}
