using System.Collections.Generic;
using UnityEngine;

public class BallAnimatorTool : MonoBehaviour
{
    [SerializeField] private bool _canMove = false;
    [SerializeField] private float _totalTime = 5.0f;
    [SerializeField] private Transform _ballTransform = null;
    [SerializeField] private List<Transform> _nodes = new List<Transform>();

    private float _elapsedTime = 0.0f;
    private int _currentNodeIndex = 0;

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

        float t = Mathf.Clamp01(_elapsedTime / _totalTime);
        _ballTransform.position = Vector3.Lerp(startPosition, endPosition, t) + transform.up * Mathf.Sin(t * Mathf.PI) * 0.1f;
    }

}
