using System;
using UnityEngine;
using NaughtyAttributes;

public class GolfBall : MonoBehaviour
{
    public bool IsIdle
    {
        get => isIdle;
        set => isIdle = value;
    }

    [Header("References")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material rolling;
    [SerializeField] private Material idle;

    [Header("Physics")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float stopThreshold = 0.01f;
    [SerializeField, ReadOnly] private bool isIdle;

    private new SphereCollider collider;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        try
        {
            collider = GetComponent<SphereCollider>();
            rigidbody = GetComponent<Rigidbody>();
        }
        catch(NullReferenceException ex)
        {
            Debug.LogException(ex);
        }
    }

    private void Update()
    {
        HandleSpeed();
        BallMesh();
    }

    private void BallMesh()
    {
        meshRenderer.material = isIdle ? idle : rolling;
    }

    private void HandleSpeed()
    {
        isIdle = rigidbody.velocity.magnitude <= stopThreshold ? true : false;

        if(isIdle)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        if(rigidbody.velocity.magnitude > maxSpeed)
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
    }

    private void OnCollisionExit(Collision collision)
    {
        Putter putter = collision.gameObject.GetComponent<Putter>();

        if (putter != null)
            rigidbody.AddForce(transform.forward * putter.Force, ForceMode.Impulse);
    }
}
