using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Putter : MonoBehaviour
{
    #region Getters/Setters

    public BoxCollider Collider
    {
        get => collider;
        set => collider = value;
    }

    public float Force
    {
        get => force;
        set => force = value;
    } 

    #endregion

    [Header("References")]
    [SerializeField] private XRRayInteractor leftRay;
    [SerializeField] private XRRayInteractor rightRay;
    [SerializeField] private new BoxCollider collider;

    [Header("Debugging")]
    [SerializeField] private bool leftSelect;
    [SerializeField] private bool rightSelect;
    [SerializeField] private bool isHeld;

    [Header("Inputs")]
    [SerializeField] private InputActionReference leftReference;
    [SerializeField] private InputActionReference rightReference;

    private new Rigidbody rigidbody;
    private XRGrabInteractable interactable;

    private float force;

    private GolfBall golfBall;

    private void Awake()
    {
        try
        {
            golfBall = FindObjectOfType<GolfBall>();

            rigidbody = GetComponent<Rigidbody>();
            interactable = GetComponent<XRGrabInteractable>();
        }
        catch(NullReferenceException ex)
        {
            Debug.LogException(ex);
        }
    }

    private void Update()
    {
        force = rigidbody.velocity.magnitude;
     
        GetInputs();
        EnableHit();
    }

    private void EnableHit()
    {
        if (isHeld && (leftSelect || rightSelect))
            collider.enabled = true;
        else
            collider.enabled = false;
    }

    private void GetInputs()
    {
        leftSelect = leftReference.action.IsPressed();
        rightSelect = rightReference.action.IsPressed();

        isHeld = interactable.isSelected;
    }
}
