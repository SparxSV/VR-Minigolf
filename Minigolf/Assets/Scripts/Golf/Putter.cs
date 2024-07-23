using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Golf
{
    public class Putter : MonoBehaviour
    {
        #region Getters/Setters

        public float Force { get; private set; }

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

        private Rigidbody rigidBody;
        private XRGrabInteractable interactable;

        private void Awake()
        {
            try
            {
                rigidBody = GetComponent<Rigidbody>();
                interactable = GetComponent<XRGrabInteractable>();
            }
            catch(NullReferenceException ex)
            {
                Debug.LogException(ex);
            }
        }

        private void Update()
        {
            Force = rigidBody.velocity.magnitude;
     
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
}
