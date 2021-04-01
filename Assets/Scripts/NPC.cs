using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private Animator animator;

    private NavMeshAgent agent;

    /// <summary>
    /// Цель
    /// </summary>
    public Transform targetPlayer;
    /// <summary>
    /// Дистанция атаки
    /// </summary>
    public float distanceAttack;
    /// <summary>
    /// Текущая дистанция до цели
    /// </summary>
    private float currentDisToTarget;
    /// <summary>
    /// Скорость NPC
    /// </summary>
    [SerializeField]
    private float speedNPC;

    private bool finish;

    #region Эффект при обнаружении игрока
    //private bool playerVisible;
    //public float time;
    //public  float timer; //
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedNPC;
        agent.angularSpeed = 500f;
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        finish = false;
    }

    private void Update()
    {
        currentDisToTarget = Vector3.Distance
            (gameObject.transform.position, targetPlayer.transform.position);

        Movement();

        if (finish)
        {
            Finish();
        }
    }

    /// <summary>
    /// Движение
    /// </summary>
    private void Movement()
    {
        if (currentDisToTarget < distanceAttack)
        {
            agent.SetDestination(targetPlayer.position);
            animator.SetBool("Run", true);

            agent.Resume();
            Effects();
        }
        else
        {
            agent.Stop();
            animator.SetBool("Run", false);
        }
    }

    private void Finish()
    {
        agent.Stop();
        animator.SetTrigger("Attack");
        agent.Stop();
    }

    public void Lost()
    {
        agent.Stop();
        animator.SetTrigger("Lost");
    }

    /// <summary>
    /// Эффект при обнаружении игрока
    /// </summary>
    private void Effects()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Dead();

            finish = true;
        }
    }
}