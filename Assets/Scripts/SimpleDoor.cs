using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public Transform door; // the part that moves
    public float moveHeight = 4f;
    public float speed = 3f;

    private bool opening = false;
    private Vector3 targetPos;

    private void Start()
    {
        if (door == null) door = transform;
        targetPos = door.position + Vector3.up * moveHeight;
    }

    private void Update()
    {
        if (opening)
        {
            door.position = Vector3.MoveTowards(door.position, targetPos, speed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        opening = true;
    }
}