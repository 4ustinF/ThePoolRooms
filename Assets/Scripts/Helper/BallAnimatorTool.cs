using System.Collections.Generic;
using UnityEngine;

public class BallAnimatorTool : MonoBehaviour
{
    [SerializeField] private bool _canMove = false;
    [SerializeField] private float _totalTime = 5.0f;
    [SerializeField] private Transform _ballTransform = null;
    [SerializeField] private List<Transform> _nodes = new List<Transform>();

    [Header("Buoyancy")]
    [SerializeField] private float _amplitude = 0.1f; // The maximum amount to move the object up and down
    [SerializeField] private float _frequency = 3.5f; // The speed at which to move the object up and down
    private float _startYPos; // The object's starting Y position

    [Header("Rotaation")]
    [SerializeField] private float _rotAmplitude = 0.1f; // The maximum amount to move the object up and down
    [SerializeField] private float _rotFrequency = 3.5f; // The speed at which to move the object up and down
    private float _startYRot; // The object's starting Y position

    private float _elapsedTime = 0.0f;
    private int _currentNodeIndex = 0;

    private void Start()
    {
        _startYPos = _ballTransform.position.y; // Store the object's starting Y position
        _startYRot = _ballTransform.localRotation.y; // Store the object's starting Y rotation
    }

    // Move to courotine
    void Update()
    {
        if (_canMove == false || _nodes.Count == 0)
        {
            return;
        }

        Transform currentNode = _nodes[_currentNodeIndex];
        Transform nextNode = _nodes[(_currentNodeIndex + 1) % _nodes.Count];

        Vector3 startPosition = currentNode.position;
        Vector3 endPosition = nextNode.position;

        float distance = Vector3.Distance(startPosition, endPosition);
        float speed = distance / _totalTime;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _totalTime)
        {
            _elapsedTime = 0.0f;
            _currentNodeIndex = (_currentNodeIndex + 1) % _nodes.Count;
        }

        // Rotation
        var ballRot = _ballTransform.localRotation;
        ballRot.y = _startYRot + _rotAmplitude * Mathf.Sin(_rotFrequency * Time.time); // Calculate the new Y rotation using a sine wave to mimic x
        _ballTransform.localRotation = ballRot;

        // Position
        float t = Mathf.Clamp01(_elapsedTime / _totalTime);
        Vector3 ballPos = Vector3.Lerp(startPosition, endPosition, t) + transform.up * Mathf.Sin(t * Mathf.PI) * 0.1f;
        ballPos.y = _startYPos + _amplitude * Mathf.Sin(_frequency * Time.time); // Calculate the new Y position using a sine wave to mimic Buoyancy
        _ballTransform.position = ballPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 verticalOffset = Vector3.up * 1.0f;
        for (int i = 0; i < _nodes.Count - 1; ++i)
        {
            Gizmos.DrawLine(_nodes[i].position + verticalOffset, _nodes[i + 1].position + verticalOffset);
        }
    }
}
