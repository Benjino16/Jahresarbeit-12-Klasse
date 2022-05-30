using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarToKeyboard : MonoBehaviour
{
    CarController car;

    private void Awake()
    {
        car = GetComponent<CarController>();
    }

    private void Update()
    {
        car.Drive(Input.GetAxisRaw("Vertical"), -Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
    }
}
