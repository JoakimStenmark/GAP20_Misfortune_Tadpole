using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class SpriteScaler : MonoBehaviour
{
    public float circleCastRadius = 1;
    public bool hasHit;

    public Vector2 defaultScale = new Vector2(1, 1);
    public Vector2 squishScale = new Vector2(1.2f, 0.8f);

    public LayerMask mask;

    Tween landWobble;

    public float time, strength;
    public int vibrato;
    public float randomness;

    private void Start()
    {
        landWobble = transform.DOShakeScale(time, strength, vibrato, randomness, true)
            .SetAutoKill(false)
            .SetRelative(true);
            

    }

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

        //Debug.Log(landWobble.IsActive());       
    }

    private void SetDefaultScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, Time.deltaTime * 10);
    }

    private void ImpactSquish()
    {       
        transform.localScale = Vector3.Lerp(transform.localScale, squishScale, Time.deltaTime * 10);
        transform.DOShakeScale(time, strength, vibrato, randomness, true);

    }

    public void JumpWobble()
    {
        transform.DOComplete();           
        transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, 1f);
        transform.DOShakeScale(time, strength, vibrato, randomness, true);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleCastRadius);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
