using UnityEngine;

public class Start_Finish_Point : MonoBehaviour
{
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject finishPoint;

    public bool startPos;

    private void Start()
    {
        if (startPos)
        {
            startPoint.SetActive(true);
            finishPoint.SetActive(false);

            GameObject.FindGameObjectWithTag("Player").transform.position = startPoint.transform.position;
        }

        else
        {
            startPoint.SetActive(false);
            finishPoint.SetActive(true);
        }
    }
}