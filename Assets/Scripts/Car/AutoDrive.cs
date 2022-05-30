using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDrive : CarController
{
    [SerializeField] float speed;



    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.up * speed * Time.deltaTime;
    }
}
