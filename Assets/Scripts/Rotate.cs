using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * speed, 0f, Space.Self);
    }
}
