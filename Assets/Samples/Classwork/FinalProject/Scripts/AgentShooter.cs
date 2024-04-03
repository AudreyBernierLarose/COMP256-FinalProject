using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentShooter : Agent
{
    private Rigidbody rb;
    private float x_min = -35f;
    private float x_max = 35f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Transform Target;    

    public override void OnEpisodeBegin()
    {
        Target.localPosition = new(Random.Range(-35f, 35f), 10f, -40f);

        Vector3 initialPosition = transform.localPosition;
        if (initialPosition.x < x_min || initialPosition.x > x_max)
            initialPosition = new(0f, 1f, 20f);
        
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.localPosition = initialPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rb.velocity.x);
    }

    public float force_multiplier = 15;

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 direction_to_target = Target.localPosition - transform.localPosition;
        direction_to_target.y = 0f; 
        direction_to_target.z = 0f;
        direction_to_target.Normalize();

        Vector3 control_signal = Vector3.zero;
        control_signal.x = direction_to_target.x;
        rb.AddForce(control_signal * force_multiplier);

        float x_difference = Mathf.Abs(transform.localPosition.x - Target.localPosition.x);
        if (x_difference < 0.1f) {
            SetReward(1.0f);
            EndEpisode();
        }
        else if (transform.localPosition.x < x_min || transform.localPosition.x > x_max)        
            EndEpisode();
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
    }
}
