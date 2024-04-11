using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentShooter : Agent
{
    [SerializeField] private ScoreManager _scoreManager;
    private Rigidbody rb;
    private float x_min = -35f;
    private float x_max = 35f;

    [SerializeField] AudioSource _enemyAudioSource;
    [SerializeField] AudioClip _enemyHitTargetSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Transform target;    

    public override void OnEpisodeBegin()
    {
        if (target)
            target.localPosition = new(Random.Range(-36f, 36f), 10f, -40f);

        Vector3 initialPosition = transform.localPosition;
        if (initialPosition.x < x_min || initialPosition.x > x_max)
            initialPosition = new(0f, 1f, 20f);

        if (rb) {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
        
        transform.localPosition = initialPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rb.velocity.x);
    }

    public float force_multiplier = 15;

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 direction_to_target = target.localPosition - transform.localPosition;
        direction_to_target.y = 0f; 
        direction_to_target.z = 0f;
        direction_to_target.Normalize();

        Vector3 control_signal = Vector3.zero;
        control_signal.x = direction_to_target.x;
        rb.AddForce(control_signal * force_multiplier);

        float x_difference = Mathf.Abs(transform.localPosition.x - target.localPosition.x);
        if (x_difference < 1.0f) {
            SetReward(1.0f);
            _enemyAudioSource.PlayOneShot(_enemyHitTargetSound); //play the enemy hits target sound.
            _scoreManager.IncrementEnemyScore();
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