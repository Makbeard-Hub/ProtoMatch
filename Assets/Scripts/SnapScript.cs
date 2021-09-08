using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapScript : MonoBehaviour
{
    public float snapValX = 1;
    public float snapValY = 1;
    public float snapValZ = 0;

//#if UNITY_EDITOR
    void Update()
    {
        if (snapValX != 0)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x * (1 / snapValX)) / (1 / snapValX), transform.position.y, transform.position.z);
        }
        if (snapValY != 0)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y * (1 / snapValY)) / (1 / snapValY), transform.position.z);
        }

        if (snapValZ != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z * (1 / snapValZ)) / (1 / snapValZ));
        }
    }
//#endif
}