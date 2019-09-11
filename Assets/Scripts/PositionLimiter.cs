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
        float x = transform.localPosition.x, y = transform.localPosition.y, z = transform.localPosition.z;
        if (limitX) {
            x = Mathf.Clamp(transform.localPosition.x, min.x, max.x);
        }if (limitY) {
            y = Mathf.Clamp(transform.localPosition.y, min.y, max.y);
        }if (limitZ) {
            z = Mathf.Clamp(transform.localPosition.z, min.z, max.z);
        }

        transform.localPosition = new Vector3(x, y, z);
    }
}
