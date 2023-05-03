using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField] private float _amplitude = 0.15f; // The maximum amount to move the object up and down
    [SerializeField] private float _frequency = 1.0f; // The speed at which to move the object up and down
    private float startY; // The object's starting Y position

    private void Start()
    {
        startY = transform.position.y; // Store the object's starting Y position
    }

    private void Update()
    {
        Vector3 pos = transform.position; // Get the current position
        pos.y = startY + _amplitude * Mathf.Sin(_frequency * Time.time); // Calculate the new Y position using a sine wave
        transform.position = pos; // Set the new position
    }
}
