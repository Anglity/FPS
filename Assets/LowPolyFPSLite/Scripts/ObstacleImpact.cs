using UnityEngine;

public class ObstacleImpact : MonoBehaviour
{
    [Tooltip("Sonido que se reproducir� al impactar con el obst�culo")]
    public AudioClip impactSound;

    private AudioSource audioSource;

    void Start()
    {
        // Aseg�rate de que hay un AudioSource componente y config�ralo
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
