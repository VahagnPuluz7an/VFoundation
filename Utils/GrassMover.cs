using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMover : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = new Vector3(target.position.x,0,target.position.z);
    }
}
