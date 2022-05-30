using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controller can be used to controll a car.
public class CarController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float maxReverseSpeed = -10f;
    [SerializeField] float acceleration = .1f;
    [SerializeField] float brakeStrength = .1f;
    [SerializeField] float steeringStrength = 1f;
    [SerializeField] float steeringSpeedSmoother = 4f;
    [SerializeField] float speedDrag = 0.1f;
    

    public float currentSpeed;

    float inputAccelerator;
    float inputBrake;
    float inputSteering;

    Vector2 spawnPoint;
    Quaternion spawnAngle;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
        spawnAngle = transform.rotation;
    }

    private void Update()
    {
        
        rb.MovePosition(transform.position + transform.up * currentSpeed);

        transform.Rotate(new Vector3(0, 0, (-inputSteering * steeringStrength * currentSpeed) / (Mathf.Pow(currentSpeed + 1, steeringSpeedSmoother))));
    }

    private void FixedUpdate()
    {
        if(currentSpeed < maxSpeed)
        {
            currentSpeed += -speedDrag + (inputAccelerator * acceleration) - (inputBrake * brakeStrength);
        }
        else
        {
            currentSpeed -= speedDrag + (inputBrake * brakeStrength);
        }
        if(currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }

    /// <summary>
    /// Mit dieser Funktion kann das Auto gesteuert werden. Sie dient dabei nur der Übergabe von den verschiedenen Eingabewerten.
    /// </summary>
    /// <param name="accelerator">Gas (Wert von 0 bis 1)</param>
    /// <param name="brake">Bremse (Wert von 0 bis 1)</param>
    /// <param name="steering">Lenkug des Autos (Wert von -1 bis 1)</param>
    public void Drive(float accelerator, float brake, float steering)
    {
        inputAccelerator = Mathf.Clamp(accelerator, 0, 1);
        inputBrake = Mathf.Clamp(brake, 0, 1);
        inputSteering = Mathf.Clamp(steering, -1, 1);
    }

    public void Reset()
    {
        //Setzt das Auto auf die Ursprungsposition zurück
        transform.position = spawnPoint;
        transform.rotation = spawnAngle;

        //Setzt die Velocity des Autos auf 0 (mögliche veränderung durch Kollision)
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;

        //Setzt die aktuelle Geschwindigkeit des Autos auf 0
        currentSpeed = 0f;

        //Setzt die Eingabewerte auf 0 zurück
        inputAccelerator = 0f;
        inputBrake = 0f;
        inputSteering = 0f;
    }
}
