using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MainLogic : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    
    public void Awake()
    {
        _splineContainer = GetComponentInChildren<SplineContainer>();
    }

    private float _parameter;
    private SplineContainer _splineContainer;
    
    public void Update()
    {
        _parameter += Time.deltaTime;
        if (_parameter > 1f)
            _parameter -= 1f;
        
        _target.position = _splineContainer.Spline.EvaluatePosition(_parameter);
    }
}
