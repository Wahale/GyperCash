using UnityEngine;

public class CameraMov : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}