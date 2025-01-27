using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform _seasonsContainer;
    [SerializeField] private Transform[] _seasons;
    [SerializeField] private Spline _spline;
    [SerializeField] private HorizontalDragInput _input;
    [Space] [SerializeField] private int _monthsOffset;
    [SerializeField] private ScrollRect _monthsRect;
    [Space]
    [SerializeField, ReadOnly(true)]
    private float _parameter;
    #endregion
    
    
    
    #region Input
    public void Awake() => _input.OnHorizontalDrag += OnDrag;
    public void OnDestroy() =>  _input.OnHorizontalDrag -= OnDrag;
    private void OnDrag(float xDelta) => AlterParameter(xDelta);
    #endregion
    
    
    
    #region Seasons & Months
    public void Start()
    {
        UpdateSeasons();
        UpdateMonths();
    }
    
    private void AlterParameter(float delta)
    {
        _parameter += delta;
        if (_parameter < 0f)
            _parameter += 1f;
        
        UpdateSeasons();
        UpdateMonths();
    }

    private void UpdateSeasons()
    {
        var offset = 1f/_seasons.Length;
        for (int i = 0; i < _seasons.Length; i++)
        {
            var t = (_parameter + i*offset) % 1f;
            _seasons[i].position = _spline.EvaluatePosition(t);
        }
    }

    private void UpdateMonths()
    {
        var offset = 1f/_monthsRect.content.childCount;
        var t = _parameter - _monthsOffset * offset;
        _monthsRect.verticalNormalizedPosition = 1f - t%1f;
    }
    #endregion

    

#if UNITY_EDITOR
    public void OnValidate()
    {
        _spline = GetComponentInChildren<SplineContainer>().Spline;

        _seasons = new Transform[_seasonsContainer.childCount];
        for(int i = 0; i < _seasonsContainer.childCount; i++)
            _seasons[i] = _seasonsContainer.GetChild(i);

        _monthsRect = GetComponentInChildren<ScrollRect>();

        _input = GetComponentInChildren<HorizontalDragInput>();
    }
#endif
}
