using System;
using UnityEngine;

namespace Tools
{
    [RequireComponent(typeof(Rigidbody))]
    public class Gravity : MonoBehaviour
    {
        private const float GlobalGravity = -9.81f;

        [SerializeField] private float gravityScale = 1.0f;

        private Rigidbody rb;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        private void FixedUpdate()
        {
            var gravity = GlobalGravity * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
