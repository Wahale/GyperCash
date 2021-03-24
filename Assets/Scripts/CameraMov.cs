using UnityEngine;

public class CameraMov : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}