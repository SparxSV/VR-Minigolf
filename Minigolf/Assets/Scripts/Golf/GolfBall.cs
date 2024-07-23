using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Golf
{
    public class GolfBall : MonoBehaviour
    {
        #region Getters/Setters
    
        public bool IsIdle
        {
            get => isIdle;
            set => isIdle = value;
        } 

        #endregion

        [Header("References")] 
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private Material rolling;
        [SerializeField] private Material idle;

        [Header("Physics")]
        [SerializeField] private float maxSpeed;
        [SerializeField] private float stopThreshold = 0.01f;

        [Header("Lerping")]
        [SerializeField, Range(.1f, 5f)] private float tweenTime = 1f;
        [SerializeField] private AnimationCurve tweenCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Debugging")]
        [SerializeField, ReadOnly] private bool isIdle;
        [SerializeField, ReadOnly] private Vector3 savedPos;
        [SerializeField, ReadOnly] private Quaternion savedRot;

        private new SphereCollider collider;
        private Rigidbody rigidBody;
        private MeshRenderer meshRenderer;

        private Transform ballSavedPosition;
    
        private void Awake()
        {
            try
            {
                meshRenderer = GetComponent<MeshRenderer>();
                
                rigidBody = GetComponent<Rigidbody>();
                collider = GetComponent<SphereCollider>();
            }
            catch(NullReferenceException ex)
            {
                Debug.LogException(ex);
            }
        }

        private void Start()
        {
            PositionSaving();

            //collider.contactOffset = 0.01f;
        }

        private void Update()
        {
            HandleSpeed();
            PositionSaving();
        
            meshRenderer.material = isIdle ? idle : rolling;
        }

        public IEnumerator ResetPosition()
        {
            var timer = 0.0f;

            var startPos = transform.position;
            var endPos = savedPos;

            collider.enabled = false;
            trailRenderer.emitting = false;
        
            while(timer < tweenTime)
            {
                var factor = Mathf.Clamp01(timer / tweenTime);
                var t = tweenCurve.Evaluate(factor);

                transform.position = Vector3.Lerp(startPos, endPos, t);

                yield return null;

                timer += Time.deltaTime;
            }

            Debug.Log($"Reset to {savedPos}");

            collider.enabled = true;
            trailRenderer.emitting = true;
            
            transform.position = endPos;
            transform.localRotation = savedRot;

            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }

        private void PositionSaving()
        {
            if (!isIdle) return;
            
            savedPos = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            savedRot = transform.localRotation;
        }

        private void HandleSpeed()
        {
            isIdle = rigidBody.velocity.magnitude <= stopThreshold;

            if(isIdle)
            {
                rigidBody.velocity = Vector3.zero;
                rigidBody.angularVelocity = Vector3.zero;
            }

            if(rigidBody.velocity.magnitude > maxSpeed)
                rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }

        private void OnCollisionExit(Collision collision)
        {
            var putter = collision.gameObject.GetComponent<Putter>();

            if (putter != null)
                rigidBody.AddForce(transform.forward * putter.Force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Green"))
                StartCoroutine(ResetPosition());
        }
    }
}
