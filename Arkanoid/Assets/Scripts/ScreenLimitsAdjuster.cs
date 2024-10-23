using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLimitsAdjuster : MonoBehaviour
{
    [SerializeField] private List<BoxCollider2D> colliders;
    public void AdjustColliderSize(RectTransform canvasTransform)
    {
        Vector2 size = new Vector2(canvasTransform.rect.width,canvasTransform.rect.height);
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].gameObject.CompareTag("Death") || colliders[i].gameObject.CompareTag("Up"))
            {
                colliders[i].size = new Vector2(size.x, 20);
                colliders[i].offset = canvasTransform.rect.center;
            }
            else if (colliders[i].gameObject.CompareTag("Side"))
            {
                colliders[i].size = new Vector2(20, size.y);
                colliders[i].offset = canvasTransform.rect.center;

            }
       
        }
        
    }
    
}
