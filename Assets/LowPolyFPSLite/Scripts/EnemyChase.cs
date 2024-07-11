using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NaturalEnemyChase : MonoBehaviour
{
    public Transform target;  // El objetivo que el enemigo perseguirá, típicamente el jugador.
    public float chaseDistance = 10.0f;  // Distancia a partir de la cual el enemigo comenzará a perseguir al jugador.
    public float stopDistance = 2.0f;  // Distancia a la que el enemigo debe detenerse del jugador.
    public float speed = 3.5f;  // Velocidad a la que el enemigo se moverá hacia el jugador.
    public float damage = 10f;  // Daño que el enemigo inflige al jugador.
    public float damageInterval = 1.0f;  // Intervalo de tiempo entre cada golpe al jugador.
    private Vector3 initialPosition;  // Posición inicial del enemigo

    private NavMeshAgent agent;
    private Animator animator;
    private float nextDamageTime = 0f;

    [Header("Audio Settings")]
    public AudioClip zombieSound;  // Sonido del zombie
    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        agent.stoppingDistance = stopDistance;
        agent.speed = speed;

        StartCoroutine(CheckDistanceAndAct()); // Asegúrate de iniciar la corrutina


    }

    IEnumerator CheckDistanceAndAct()
    {
        while (true)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget < chaseDistance)
            {
                ChasePlayer(distanceToTarget);
            }
            else
            {
                ReturnToInitialPosition();
            }

            yield return new WaitForSeconds(0.2f);  // Check every 0.2 seconds
        }
    }

    void ChasePlayer(float distanceToTarget)
    {
        if (!agent.hasPath || agent.destination != target.position)
        {
            agent.SetDestination(target.position);
            SetAnimationState("isRunning");
            PlayZombieSound();
        }

        if (distanceToTarget <= stopDistance && Time.time >= nextDamageTime)
        {
            TryDamagePlayer();
        }
    }

    void ReturnToInitialPosition()
    {
        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            if (!agent.hasPath || agent.destination != initialPosition)
            {
                agent.SetDestination(initialPosition);
                SetAnimationState("isWalking");
            }
        }
        else
        {
            agent.ResetPath();
            SetAnimationState("isIdle");
        }
    }

    void TryDamagePlayer()
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            nextDamageTime = Time.time + damageInterval;
            PlayZombieSound();
        }
    }

    void SetAnimationState(string state)
    {
        animator.SetBool("isIdle", state == "isIdle");
        animator.SetBool("isWalking", state == "isWalking");
        animator.SetBool("isRunning", state == "isRunning");
    }

    void PlayZombieSound()
    {
        if (zombieSound != null && audioSource != null)
        {
            if (!audioSource.isPlaying) // Evita que el sonido se reproduzca en bucle continuo
            {
                audioSource.PlayOneShot(zombieSound);
            }
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing for zombie sound");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
