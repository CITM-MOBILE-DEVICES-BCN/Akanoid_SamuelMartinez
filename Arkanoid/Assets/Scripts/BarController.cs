using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField] private RectTransform maxPosition;
    [SerializeField] private RectTransform minPosition;

    public bool isFollowingABall;
    public RectTransform ballToFollowPosition;

    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void AdjustBarPosition(float value)
    {
        float newX = Mathf.Lerp(minPosition.position.x, maxPosition.position.x, value);
        transform.DOMoveX(newX, 0.3f);
    }

    public void FollowPosition(RectTransform target)
    {
        rectTransform.DOMoveX(target.position.x, 0.3f);
    }

    private void Update()
    {
        if (isFollowingABall && ballToFollowPosition != null)
        {
            FollowPosition(ballToFollowPosition);           
        }
    }
}
