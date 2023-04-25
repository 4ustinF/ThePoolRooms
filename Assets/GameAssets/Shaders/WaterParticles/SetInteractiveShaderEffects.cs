using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInteractiveShaderEffects : MonoBehaviour
{
    [SerializeField] private RenderTexture _rt = null;
    [SerializeField] private Transform _target = null;
    [SerializeField] private Camera _camera = null;

    void Awake()
    {
        Shader.SetGlobalTexture("_GlobalEffectRT", _rt);
        Shader.SetGlobalFloat("_OrthographicCamSize", _camera.orthographicSize);
    }

    private void Update()
    {
        //transform.position = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);
        transform.position = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);
        Shader.SetGlobalVector("_Position", transform.position);
    }


}