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
    [SerializeField] private AnimationCurve _xRotCurve = new AnimationCurve();
    [SerializeField] private AnimationCurve _yRotCurve = new AnimationCurve();
    [SerializeField] private AnimationCurve _zRotCurve = new AnimationCurve();

    private int bounceCount = 0;
    private bool _isRecording = false;

    [SerializeField] private Animator _ballAnimator = null;


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

    public void StartEnterTunnelAnim()
    {
        _xPosCurve = new AnimationCurve();
        _yPosCurve = new AnimationCurve();
        _zPosCurve = new AnimationCurve();
        _xRotCurve = new AnimationCurve();
        _yRotCurve = new AnimationCurve();
        _zRotCurve = new AnimationCurve();
        StartCoroutine(EnterTunnelRoutine(0, 1));
    }

    public void StartExitTunnelAnim()
    {
        _xPosCurve = new AnimationCurve();
        _yPosCurve = new AnimationCurve();
        _zPosCurve = new AnimationCurve();
        _xRotCurve = new AnimationCurve();
        _yRotCurve = new AnimationCurve();
        _zRotCurve = new AnimationCurve();
        StartCoroutine(ExitTunnelRoutine(0, 1));
    }

    public void ObtainNodes()
    {
        _nodes.Clear();
        foreach (Transform child in transform)
        {
            _nodes.Add(child);
        }
    }

    public void SetAnimationClips()
    {
        _clipToEdit.ClearCurves();
        if (_xPosCurve.keys.Length > 0)
        {
            _clipToEdit.SetCurve("", typeof(Transform), "localPosition.x", _xPosCurve);
        }

        if (_yPosCurve.keys.Length > 0)
        {
            _clipToEdit.SetCurve("", typeof(Transform), "localPosition.y", _yPosCurve);
        }

        if (_zPosCurve.keys.Length > 0)
        {
            _clipToEdit.SetCurve("", typeof(Transform), "localPosition.z", _zPosCurve);
        }

        if (_xRotCurve.keys.Length > 0)
        {
            _clipToEdit.SetCurve("", typeof(Transform), "localEulerAngles.x", _xRotCurve);
        }

        if (_yRotCurve.keys.Length > 0)
        {
            _clipToEdit.SetCurve("", typeof(Transform), "localEulerAngles.y", _yRotCurve);
        }

        if (_zRotCurve.keys.Length > 0)
        {
            _clipToEdit.SetCurve("", typeof(Transform), "localEulerAngles.z", _zRotCurve);
        }
    }

    private IEnumerator EnterTunnelRoutine(int startIndex, int endIndex)
    {
        int currentFrame = 0;
        float elapsedTime = 0.0f;
        Vector3 startPosition = _nodes[startIndex].position;
        Vector3 endPosition = _nodes[endIndex].position;

        _xPosCurve = new AnimationCurve();
        _yPosCurve = new AnimationCurve();
        _zPosCurve = new AnimationCurve();
        _xRotCurve = new AnimationCurve();
        _yRotCurve = new AnimationCurve();
        _zRotCurve = new AnimationCurve();

        while (true)
        {
            float t = Mathf.Clamp01(elapsedTime / _totalTime);
            float inverseT = 1.0f - Mathf.Sin((t * Mathf.PI) * 0.5f); // 1.0f - t;
            inverseT = Mathf.Clamp(inverseT, 0.3f, 1.0f);

            // Rotation
            var ballRot = _ballTransform.localRotation;
            ballRot.x = _startYRot + (_rotAmplitude * +Mathf.Sin(_rotFrequency * Time.time) * inverseT); // Calculate the new X rotation using a sine wave to mimic x
            ballRot.y = _startYRot + (_rotAmplitude * +Mathf.Sin(_rotFrequency * Time.time) * inverseT); // Calculate the new Y rotation using a sine wave to mimic x
            ballRot.z = _startYRot + (_rotAmplitude * -Mathf.Sin(_rotFrequency * Time.time) * inverseT); // Calculate the new Z rotation using a sine wave to mimic x
            _ballTransform.localRotation = ballRot;

            // Position
            Vector3 ballPos = Vector3.Lerp(startPosition, endPosition, t) + transform.up * Mathf.Sin(t * Mathf.PI) * 0.1f;
            ballPos.y = _startYPos + (_amplitude * Mathf.Sin(_frequency * Time.time) * inverseT); // Calculate the new Y position using a sine wave to mimic Buoyancy
            _ballTransform.position = ballPos;

            // Position Frames
            float keyPos = (float)currentFrame / 60.0f;
            _xPosCurve.AddKey(keyPos, _ballTransform.position.x);
            _yPosCurve.AddKey(keyPos, _ballTransform.position.y);
            _zPosCurve.AddKey(keyPos, _ballTransform.position.z);

            // Rotation Frames
            float xRot = _ballTransform.localEulerAngles.x;
            float yRot = _ballTransform.localEulerAngles.y;
            float zRot = _ballTransform.localEulerAngles.z;

            if (xRot > 300.0f)
            {
                xRot -= 360.0f;
            }

            if (yRot > 300.0f)
            {
                yRot -= 360.0f;
            }

            if (zRot > 300.0f)
            {
                zRot -= 360.0f;
            }

            _xRotCurve.AddKey(keyPos, xRot);
            _yRotCurve.AddKey(keyPos, yRot);
            _zRotCurve.AddKey(keyPos, zRot);

            // Exit Condition
            if (elapsedTime >= _totalTime)
            {
                Debug.Log("Time exit");
                break;
            }

            ++currentFrame;
            elapsedTime += 0.01f;

            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator ExitTunnelRoutine(int startIndex, int endIndex, int currentFrame = 0)
    {
        float elapsedTime = 0.0f;
        Vector3 startPosition = _nodes[startIndex].position;
        Vector3 endPosition = _nodes[endIndex].position;

        while (true)
        {
            float t = Mathf.Clamp01(elapsedTime / _totalTime);

            // Rotation
            var ballRot = _ballTransform.localRotation;
            ballRot.x = _startYRot + _rotAmplitude * +Mathf.Sin(_rotFrequency * Time.time); // Calculate the new X rotation using a sine wave to mimic x
            ballRot.y = _startYRot + _rotAmplitude * +Mathf.Sin(_rotFrequency * Time.time); // Calculate the new Y rotation using a sine wave to mimic x
            ballRot.z = _startYRot + _rotAmplitude * -Mathf.Sin(_rotFrequency * Time.time); // Calculate the new Z rotation using a sine wave to mimic x
            _ballTransform.localRotation = ballRot;

            // Position
            Vector3 ballPos = Vector3.Lerp(startPosition, endPosition, t) + transform.up * Mathf.Sin(t * Mathf.PI) * 0.1f;
            ballPos.y = _startYPos + _amplitude * Mathf.Sin(_frequency * Time.time); // Calculate the new Y position using a sine wave to mimic Buoyancy
            _ballTransform.position = ballPos;

            // Position Frames
            float keyPos = (float)currentFrame / 60.0f;
            _xPosCurve.AddKey(keyPos, _ballTransform.position.x);
            _yPosCurve.AddKey(keyPos, _ballTransform.position.y);
            _zPosCurve.AddKey(keyPos, _ballTransform.position.z);

            // Rotation Frames
            float xRot = _ballTransform.localEulerAngles.x;
            float yRot = _ballTransform.localEulerAngles.y;
            float zRot = _ballTransform.localEulerAngles.z;

            if (xRot > 300.0f)
            {
                xRot -= 360.0f;
            }

            if (yRot > 300.0f)
            {
                yRot -= 360.0f;
            }

            if (zRot > 300.0f)
            {
                zRot -= 360.0f;
            }

            _xRotCurve.AddKey(keyPos, xRot);
            _yRotCurve.AddKey(keyPos, yRot);
            _zRotCurve.AddKey(keyPos, zRot);

            if (elapsedTime >= _totalTime)
            {
                Debug.Log("Time exit");
                break;
            }

            ++currentFrame;
            elapsedTime += 0.01f;

            yield return new WaitForSeconds(0.01f);
        }

        if (endIndex < _nodes.Count - 1)
        {
            bounceCount++;
            if (bounceCount == 1)
            {
                _totalTime *= 0.25f;
            }
            else if (bounceCount == 2)
            {
                _totalTime *= 1.25f;
            }

            StartCoroutine(ExitTunnelRoutine(startIndex + 1, endIndex + 1, currentFrame));
        }
    }

    public void StartRecording()
    {
        // -13.7, 13.89, 14.377 - Roof
        // -14, 1.75, 10 - Idle 1
        // -0.72, 1.75, -2.53 - Idle 2
        // -2, 1.75, -2.53 - end of tunnel

        _isRecording = true;
        _ballTransform.gameObject.SetActive(true);
        StartCoroutine(RecordBall());
    }

    public void StopRecording()
    {
        _isRecording = false;
    }

    private IEnumerator RecordBall()
    {
        int currentFrame = 0;
        float rotationThreshold = 100.0f;
        Quaternion previousRotation = _ballTransform.rotation;

        float xPrevRot = _ballTransform.localEulerAngles.x;
        float yPrevRot = _ballTransform.localEulerAngles.y;
        float zPrevRot = _ballTransform.localEulerAngles.z;

        yield return null;

        while (_isRecording)
        {
            // Position Frames
            float keyPos = (float)currentFrame / 60.0f;
            _xPosCurve.AddKey(keyPos, _ballTransform.position.x);
            _yPosCurve.AddKey(keyPos, _ballTransform.position.y);
            _zPosCurve.AddKey(keyPos, _ballTransform.position.z);

            // Rotation Frames
            float xRot = _ballTransform.localEulerAngles.x;
            float yRot = _ballTransform.localEulerAngles.y;
            float zRot = _ballTransform.localEulerAngles.z;

            if(Mathf.Abs(xRot - xPrevRot) > rotationThreshold)
            {
                xRot -= 360.0f;
            }
            if (Mathf.Abs(yRot - yPrevRot) > rotationThreshold)
            {
                yRot -= 360.0f;
            }
            if (Mathf.Abs(zRot - zPrevRot) > rotationThreshold)
            {
                zRot -= 360.0f;
            }

            _xRotCurve.AddKey(keyPos, xRot);
            _yRotCurve.AddKey(keyPos, yRot);
            _zRotCurve.AddKey(keyPos, zRot);

            // Set Prev
            xPrevRot = xRot;
            yPrevRot = yRot;
            zPrevRot = zRot;

            ++currentFrame;
            yield return new WaitForSeconds(0.01f);
        }

    }

    public void PlayBallAnimation()
    {
        _ballAnimator?.Play("Idle");
        _ballAnimator?.Play(_clipToEdit.name);
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
