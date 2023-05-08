using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimatorTool : MonoBehaviour
{
    [SerializeField] private float _totalTime = 5.0f;
    [SerializeField] private bool _isIdle = false;
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

    [Header("Aniamtion")]
    [SerializeField] private AnimationClip _clipToEdit = null;
    [SerializeField] private AnimationCurve _xPosCurve = new AnimationCurve();
    [SerializeField] private AnimationCurve _yPosCurve = new AnimationCurve();
    [SerializeField] private AnimationCurve _zPosCurve = new AnimationCurve();
    [SerializeField] private AnimationCurve _yRotCurve = new AnimationCurve();

    //Idle
    private int _idleCount = 0;

    private void Start()
    {
        _startYRot = _ballTransform.localRotation.y; // Store the object's starting Y rotation
        _startYPos = _ballTransform.position.y; // Store the object's starting Y position
    }

    private void FixedUpdate()
    {
        if (_isIdle)
        {
            // Rotation
            var ballRot = _ballTransform.rotation;
            ballRot.y = _startYRot + _rotAmplitude * Mathf.Sin(_rotFrequency * Time.time); // Calculate the new Y rotation using a sine wave to mimic x
            _ballTransform.rotation = ballRot;

            // Position
            Vector3 pos = _ballTransform.position; // Get the current position
            pos.y = _startYPos + _amplitude * Mathf.Sin(_frequency * Time.time); // Calculate the new Y position using a sine wave
            _ballTransform.position = pos; // Set the new position

            _xPosCurve.AddKey((float)_idleCount / 60.0f, _ballTransform.position.x);
            _yPosCurve.AddKey((float)_idleCount / 60.0f, _ballTransform.position.y);
            _zPosCurve.AddKey((float)_idleCount / 60.0f, _ballTransform.position.z);
            _yRotCurve.AddKey((float)_idleCount / 60.0f, _ballTransform.localEulerAngles.y);

            if (++_idleCount > 180)
            {
                _isIdle = false;
                _idleCount = 0;
                return;
            }

        }
    }

    public void StartAnim()
    {
        StartCoroutine(AnimRoutine(0, 1));
    }

    public void ObtainNodes()
    {
        _nodes.Clear();
        foreach (Transform child in transform)
        {
            _nodes.Add(child);
        }
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

            Animator animator;
            AnimationClip animClip;

            

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

    public void SetAnimationClips()
    {
        _clipToEdit.ClearCurves();
        if (_xPosCurve.keys.Length > 0) {
            _clipToEdit.SetCurve("", typeof(Transform), "localPosition.x", _xPosCurve);
        }

        if (_yPosCurve.keys.Length > 0) {
            _clipToEdit.SetCurve("", typeof(Transform), "localPosition.y", _yPosCurve);
        }

        if (_zPosCurve.keys.Length > 0) {
            _clipToEdit.SetCurve("", typeof(Transform), "localPosition.z", _zPosCurve);
        }

        if (_yRotCurve.keys.Length > 0) {
            _clipToEdit.SetCurve("", typeof(Transform), "localEulerAngles.y", _yRotCurve);
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
