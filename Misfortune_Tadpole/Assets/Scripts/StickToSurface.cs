using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class StickToSurface : MonoBehaviour
{
    Vector3 deltaPos;
    Rigidbody2D rb2d;
    public RotatorRotation rotatorRotation;
    public bool stuck;
    float timer;
    public const float STICK_COOLDOWN = 0.001f;
    public float unstuckForceOut = 100f;
    public float unstuckForceForward = 50f;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timer = (timer < 0) ? 0 : timer;

        if (Input.GetButtonDown("Jump") && stuck)
        {
            UnStuck();
        }


        deltaPos = transform.position;
    }

    private void UnStuck()
    {
        timer = STICK_COOLDOWN;
        stuck = false;
        rb2d.isKinematic = false;
        GameObject oldParent = transform.parent.gameObject;
        transform.SetParent(null);

        Eject(oldParent);
    }

    private void Eject(GameObject oldParent)
    {

        Vector3 flyOffDirection = Vector3.zero;
        flyOffDirection = transform.position - oldParent.transform.position;
        flyOffDirection = flyOffDirection.normalized;

        Vector3 extraDirection = Vector3.zero;
        extraDirection = transform.position - deltaPos;
        extraDirection = extraDirection.normalized;
        Debug.Log(extraDirection);


        rb2d.AddForce(flyOffDirection * unstuckForceOut, ForceMode2D.Impulse);
        rb2d.AddForce(extraDirection * unstuckForceForward, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timer > 0) return;

        if (collision.CompareTag("Sticky"))
        {
            GetStuck(collision.transform);
            rotatorRotation = collision.gameObject.GetComponent<RotatorRotation>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!stuck) return;

        if (collision.CompareTag("Sticky"))
        {
            UnStuck();
            Debug.LogWarning("Got unstuck from other source");
        }
    }

    private void GetStuck(Transform parent)
    {
        transform.SetParent(parent);
        stuck = true;
        rb2d.isKinematic = true;
        rb2d.velocity = Vector3.zero;
    }
}
