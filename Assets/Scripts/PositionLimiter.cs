using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
public class PositionLimiter : MonoBehaviour
{
    [SerializeField] Vector3 min;
    [SerializeField] Vector3 max;
    [SerializeField] bool limitX, limitY, limitZ;

    void LateUpdate()
    {
        float x = transform.position.x, y = transform.position.y, z = transform.position.z;
        if (limitX) {
            x = Mathf.Clamp(transform.position.x, min.x, max.x);
        }if (limitY) {
            y = Mathf.Clamp(transform.position.y, min.y, max.y);
        }if (limitZ) {
            z = Mathf.Clamp(transform.position.z, min.z, max.z);
        }

        transform.position = new Vector3(x, y, z);
    }
}
