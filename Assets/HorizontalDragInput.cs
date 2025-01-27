using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HorizontalDragInput : MonoBehaviour, IDragHandler
{
    public event Action<float> OnHorizontalDrag;

    [SerializeField, Range(1f, 10f)] private float _sensitivity;
    private const float DAMPER = 1000f;
    
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.delta.x == 0f)
            return;

        OnHorizontalDrag?.Invoke(eventData.delta.x * _sensitivity/DAMPER);
    }
}
