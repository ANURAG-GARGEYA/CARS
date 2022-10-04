using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * speed, 0f, Space.Self);
    }
}
