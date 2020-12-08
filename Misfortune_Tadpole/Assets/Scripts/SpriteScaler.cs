using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteScaler : MonoBehaviour
{
    public float circleCastRadius = 1;
    public bool hasHit;

    public Vector2 defaultScale = new Vector2(1, 1);
    public Vector2 squishScale = new Vector2(1.2f, 0.8f);

    public LayerMask mask;

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleCastRadius, Vector2.zero, 0f, mask);
        
        if (hit.collider && !hit.collider.isTrigger && !hasHit)
        {
            hasHit = true;
            ImpactSquish();
            Invoke(nameof(SetDefaultScale), 0.5f);
        }
        else if(!hit.collider)
        {
            hasHit = false;
            SetDefaultScale();
        }
        
        
    }

    private void SetDefaultScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, Time.deltaTime * 10);
    }

    private void ImpactSquish()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, squishScale, Time.deltaTime * 10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleCastRadius);
    }
}
