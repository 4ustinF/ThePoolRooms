using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimatorTool : MonoBehaviour
{
    [SerializeField] private float _totalTime = 5.0f;
    [SerializeField] private Transform _ballTransform = null;
    [SerializeField] private List<Transform> _nodes = new List<Transform>();

    [Header("Rotation")]
    [SerializeField] private float _rotAmplitude = 0.2f; // The maximum amount to move the object up and down
    [SerializeField] private float _rotFrequency = 0.5f; // The speed at which to move the object up and down
    private float _startYRot; // The object's starting Y position

    [Header("Buoyancy")]
    [SerializeField] private float _amplitude = 0.1f; // The maximum amount to move the object up and down
    [SerializeField] private float _frequency = 3.5f; // The speed at which to move the object up and down
    private float _startYPos; // The object's starting Y position

    private void Start()
    {
        _startYRot = _ballTransform.localRotation.y; // Store the object's starting Y rotation
        _startYPos = _ballTransform.position.y; // Store the object's starting Y position
    }

    public void StartAnim()
    {
        StartCoroutine(AnimRoutine(0, 1));
    }

    private IEnumerator AnimRoutine(int startIndex, int endIndex)
    {
        float elapsedTime = 0.0f;
        Vector3 startPosition = _nodes[startIndex].position;
        Vector3 endPosition = _nodes[endIndex].position;

        while (true)
        {
            elapsedTime += Time.deltaTime;

            // Rotation
            var ballRot = _ballTransform.localRotation;
            ballRot.y = _startYRot + _rotAmplitude * Mathf.Sin(_rotFrequency * Time.time); // Calculate the new Y rotation using a sine wave to mimic x
            _ballTransform.localRotation = ballRot;

            // Position
            float t = Mathf.Clamp01(elapsedTime / _totalTime);
            Vector3 ballPos = Vector3.Lerp(startPosition, endPosition, t) + transform.up * Mathf.Sin(t * Mathf.PI) * 0.1f;
            ballPos.y = _startYPos + _amplitude * Mathf.Sin(_frequency * Time.time); // Calculate the new Y position using a sine wave to mimic Buoyancy
            _ballTransform.position = ballPos;

            if (elapsedTime >= _totalTime)
            {
                break;
            }

            yield return null;
        }

        if (endIndex < _nodes.Count - 1)
        {
            StartCoroutine(AnimRoutine(startIndex + 1, endIndex + 1));
        }
    }

    public void ObtainNodes()
    {
        _nodes.Clear();
        foreach (Transform child in transform)
        {
            _nodes.Add(child);
        }
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
