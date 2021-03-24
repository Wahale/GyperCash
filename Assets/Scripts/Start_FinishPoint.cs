using UnityEngine;

public class Start_FinishPoint : MonoBehaviour
{
    /// <summary>
    /// Является ли точка стартом
    /// </summary>
    public bool startPoint;
    /// <summary>
    /// ...Финишем
    /// </summary>
    public bool finishPoint;

    /// <summary>
    /// Позиция стрта игрока
    /// </summary>
    public GameObject startPointPlayer;

    /// <summary>
    /// Финиш
    /// </summary>
    public GameObject finishPointPlayer;

    public GameObject playerPrefab;

    private void Awake()
    {
        if (startPoint)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = startPointPlayer.transform.position;
        }

        if (finishPoint)
        {
            finishPointPlayer.SetActive(true);
        }
        else
        {
            finishPointPlayer.SetActive(false);
        }
    }
}