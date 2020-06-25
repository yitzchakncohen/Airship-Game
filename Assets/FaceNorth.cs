using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceNorth : MonoBehaviour
{
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void ApplyRotation(float rotation)
    {
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }
}
