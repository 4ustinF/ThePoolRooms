using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float _rotAmplitude = 0.2f; // The maximum amount to move the object up and down
    [SerializeField] private float _rotFrequency = 0.5f; // The speed at which to move the object up and down
    private float _startYRot; // The object's starting Y rotation

    [Header("Buoyancy")]
    [SerializeField] private float _amplitude = 0.15f; // The maximum amount to move the object up and down
    [SerializeField] private float _frequency = 1.0f; // The speed at which to move the object up and down
    private float _startY; // The object's starting Y position

    private void Start()
    {
        _startYRot = transform.rotation.y; // Store the object's starting Y rotation
        _startY = transform.position.y; // Store the object's starting Y position
    }

    private void Update()
    {
        // Rotation
        var ballRot = transform.rotation;
        ballRot.y = _startYRot + _rotAmplitude * Mathf.Sin(_rotFrequency * Time.time); // Calculate the new Y rotation using a sine wave to mimic x
        transform.rotation = ballRot;

        // Position
        Vector3 pos = transform.position; // Get the current position
        pos.y = _startY + _amplitude * Mathf.Sin(_frequency * Time.time); // Calculate the new Y position using a sine wave
        transform.position = pos; // Set the new position
    }
}
