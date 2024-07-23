using System;
using UnityEngine;

namespace Objects
{
    public class Hole : MonoBehaviour
    {
        #region Getters/Setters

        /*public bool HoleCompleted
        {
            get;
        }*/

        #endregion

        [Header("References")]
        [SerializeField] private ParticleSystem celebrationParticle;

        private void Awake()
        {
            try
            {
            }
            catch(NullReferenceException ex)
            {
                Debug.LogException(ex);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GolfBall golfBall = other.GetComponent<GolfBall>();

            if (golfBall == null) return;
                
            Debug.Log("Hit");
            celebrationParticle.Play();
        }
    }
}
