using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Tools
{
    public class HandController : MonoBehaviour
    {
        [Header("Left Hand References")]
        [SerializeField] private GameObject leftPutter;
        [SerializeField] private XRInteractorLineVisual leftHandInteractor;
        [SerializeField] private LineRenderer leftLineRenderer;

        [Header("Right Hand References")]
        [SerializeField] private GameObject rightPutter;
        [SerializeField] private XRInteractorLineVisual rightHandInteractor;
        [SerializeField] private LineRenderer rightLineRenderer;

        [Header("Debugging")]
        [SerializeField] private bool isLeftHand;
        [SerializeField] private bool isRightHand;

        [Header("Inputs")]
        [SerializeField] private InputActionReference leftReference;
        [SerializeField] private InputActionReference rightReference;

        private void Start()
        {
            isRightHand = true;
            isLeftHand = false;
        }

        private void Update()
        {
            GetInputs();
            RayVisualizer();
        }

        private void RayVisualizer()
        {
            leftHandInteractor.enabled = isRightHand;
            leftLineRenderer.enabled = isRightHand;

            rightHandInteractor.enabled = isLeftHand;
            rightLineRenderer.enabled = isLeftHand;
        }

        private void GetInputs()
        {
            if (leftReference.action.IsPressed())
            {
                isLeftHand = true;
                isRightHand = false;
            }

            if(rightReference.action.IsPressed())
            {
                isLeftHand = false;
                isRightHand = true;
            }
        }
    }
}
