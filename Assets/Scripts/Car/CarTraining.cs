using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(CarController))]
public class CarTraining : Agent
{

    [SerializeField] float pointsPerUnit;
    [SerializeField] float pointsAfterCrash = -1f;
    [SerializeField] float crashMultiplier = 1f;
    [SerializeField] float goalReward = 5f;


    [SerializeField] EnviromentGenerator enviromentGenerator;
    [SerializeField] bool useSpeedObservation = true;


    CarController carController;

    float maxY;

    public void Awake()
    {
        carController = GetComponent<CarController>();
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("New Episode Begin");

        if(enviromentGenerator != null)
        {
            enviromentGenerator.Generate();
        }
        
        carController.Reset();


        maxY = transform.position.y;
    }
    private void Update()
    {
        if (transform.position.y > maxY)
        {
            AddReward((transform.position.y - maxY) * pointsPerUnit);
            maxY = transform.position.y;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (useSpeedObservation)
        {
            sensor.AddObservation(carController.currentSpeed);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        carController.Drive(actions.ContinuousActions[0], -actions.ContinuousActions[0], actions.ContinuousActions[1]);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuesActions = actionsOut.ContinuousActions;
        continuesActions[0] = Input.GetAxisRaw("Vertical");
        continuesActions[1] = Input.GetAxisRaw("Horizontal");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetReward(pointsAfterCrash - (collision.relativeVelocity.magnitude * crashMultiplier));
        EndEpisode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            SetReward(goalReward);
            EndEpisode();
        }
    }
}
