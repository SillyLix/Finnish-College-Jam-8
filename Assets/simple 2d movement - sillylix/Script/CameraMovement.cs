using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [Tooltip("Normally player")]
    [SerializeField] private GameObject Target;
    [Tooltip("offset of the camera")]
    [SerializeField] private Vector3 offset;
    [Tooltip("The speed at which the camera will follow the player.")]

    [SerializeField] private float cameraSpeed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Vector3 targetPosition = Target.transform.position + offset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * cameraSpeed);
    }
}
