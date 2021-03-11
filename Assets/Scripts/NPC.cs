using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
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

    #region Эффект при обнаружении игрока
    //private bool playerVisible;
    //public float time;
    //public  float timer; //
    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedNPC;
        agent.angularSpeed = 500f;
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        currentDisToTarget = Vector3.Distance
            (gameObject.transform.position, targetPlayer.transform.position);

        Movement();
    }

    /// <summary>
    /// Движение
    /// </summary>
    private void Movement()
    {
        if (currentDisToTarget < distanceAttack)
        {
            agent.SetDestination(targetPlayer.position);

            Effects();
        }
    }

    /// <summary>
    /// Эффект при обнаружении игрока
    /// </summary>
    private void Effects()
    {
       
    }
}