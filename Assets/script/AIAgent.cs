using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;


public class AIAgent : MonoBehaviour
{
    private AIPath aiPath; // Reference to the AIPath component
    [BoxGroup("AI Settings")]
    [SerializeField] private Transform target; // Target to move towards
    [BoxGroup("AI Settings")]
    [SerializeField] private float stoppingDistance = 0.5f; // Distance to stop from the target
    [BoxGroup("AI Settings")]
    [SerializeField] private float speed = 2f; // Speed of the AI agent
    private float currentDistance; // Current distance to the target

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aiPath = GetComponent<AIPath>(); // Get the AIPath component attached to this GameObject

    }

    // Update is called once per frame
    void Update()
    {
        aiPath.maxSpeed = speed; // Set the maximum speed of the AI agent
        aiPath.destination = target.position; // Set the initial destination to the target's position

        currentDistance = Vector2.Distance(transform.position, target.position); // Calculate the current distance to the target

        // If the distance to the target is less than the stopping distance, stop the AI agent
        if (currentDistance < stoppingDistance)
        {
            aiPath.maxSpeed = 0; // Stop the AI agent
        }
        else
        {
            aiPath.maxSpeed = speed; // Resume the AI agent's speed
        }
    }
}
