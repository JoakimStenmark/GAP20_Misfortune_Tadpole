using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteScaler : MonoBehaviour
{
    public float impactSquishX = 1.5f;
    public float impactSquishY = 0.8f;
    public float impactYOffset;
    public float circleCastRadius = 1;
    public bool hasHit;

    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        impactYOffset = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //transform.localScale = transform.localScale * Random.value;
            ImpactSquish();
        }
        SetDefaultScale();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleCastRadius, Vector2.zero, 0f, mask);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            hasHit = true;
            if (hasHit)
            {
                ImpactSquish();
                hasHit = false;
            }
        }
        else
        {
            hasHit = false;
        }
    }

    private void SetDefaultScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime);
    }

    private void ImpactSquish()
    {
        Vector3 squish = new Vector3(impactSquishX, impactSquishY,1f);
        Vector2 positionOffset = new Vector2(transform.localPosition.x, transform.localPosition.y - impactYOffset);
        transform.localScale = Vector3.Lerp(squish, Vector3.one, Time.deltaTime);
       // transform.localPosition = positionOffset;

    }

    private void OnDrawGizmos()
    {
        
    }
}
