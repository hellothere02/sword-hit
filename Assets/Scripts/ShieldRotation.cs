using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{
    [SerializeField, Range(-50, 50)] private float speedRotation;

    private void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 10 * speedRotation);
    }
}
