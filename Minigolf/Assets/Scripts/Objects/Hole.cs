using System;
using UnityEngine;

namespace Objects
{
    public class Hole : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ParticleSystem celebrationParticle;

        private void OnTriggerEnter(Collider other)
        {
            var golfBall = other.GetComponent<GolfBall>();

            if (golfBall == null) return;
                
            Debug.Log("Hit");
            celebrationParticle.Play();
        }
    }
}
