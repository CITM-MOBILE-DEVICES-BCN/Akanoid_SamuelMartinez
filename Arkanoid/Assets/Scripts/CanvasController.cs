using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridForObstacles;
    private Vector2 previousCanvasSize;
    private UnityAction<RectTransform> onChangeCanvasSize;
    private RectTransform rt;

    public void Init(UnityAction<RectTransform> onChangeCanvasSize)
    {
        this.onChangeCanvasSize = onChangeCanvasSize;
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rt.sizeDelta != previousCanvasSize)
        {
            gridForObstacles.enabled = true;
            onChangeCanvasSize?.Invoke(rt);
            previousCanvasSize = rt.sizeDelta;
        }
        else
        {
            if (gridForObstacles.isActiveAndEnabled)
            {
                gridForObstacles.enabled = false;
            }
        }
    }

    
}
