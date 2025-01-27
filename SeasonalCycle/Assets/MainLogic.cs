using System.ComponentModel;
using UnityEngine;
using UnityEngine.Splines;

public class MainLogic : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform _seasonsContainer;
    [SerializeField] private Transform[] _seasons;
    [SerializeField] private Spline _spline;
    [SerializeField] private HorizontalDragInput _input;
    [Space]
    [SerializeField, ReadOnly(true), Range(0f, 1f)]
    private float _parameter;
    #endregion
    
    
    
    #region Input
    public void Awake()
    {
        _input.OnHorizontalDrag += OnDrag;
    }

    public void OnDestroy()
    {
        _input.OnHorizontalDrag -= OnDrag;
    }

    private void OnDrag(float xDelta)
    {
        _parameter += xDelta;
    }
    #endregion

    

#if UNITY_EDITOR
    public void OnValidate()
    {
        _spline = GetComponentInChildren<SplineContainer>().Spline;

        _seasons = new Transform[_seasonsContainer.childCount];
        for(int i = 0; i < _seasonsContainer.childCount; i++)
            _seasons[i] = _seasonsContainer.GetChild(i);

        _input = GetComponentInChildren<HorizontalDragInput>();
    }
#endif
}
