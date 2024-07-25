using UnityEngine;

namespace Golf
{
    public class Hole : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ParticleSystem celebrationParticle;

        private void OnTriggerEnter(Collider other)
        {
            GolfBall golfBall = other.GetComponent<GolfBall>();

            if (golfBall == null) return;
                
            Debug.Log("Hit");
            celebrationParticle.Play();
        }
    }
}
