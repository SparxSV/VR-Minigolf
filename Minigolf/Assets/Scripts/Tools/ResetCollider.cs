using Golf;
using UnityEngine;

namespace Tools
{
    [RequireComponent(typeof(BoxCollider))]
    public class ResetCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GolfBall ball = other.GetComponent<GolfBall>();

            if(ball != null)
                StartCoroutine(ball.ResetPosition());
        }
    }
}
