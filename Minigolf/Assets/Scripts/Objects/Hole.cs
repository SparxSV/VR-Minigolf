using System;
using UnityEngine;

public class Hole : MonoBehaviour
{
    #region Getters/Setters

    public bool HoleCompleted
    {
        get;
        private set;
    }

    #endregion

    [Header("References")]
    [SerializeField] private ParticleSystem celebrationParticle;

    [Header("Debugging")]
    [SerializeField] private bool holeCompleted;

    private new BoxCollider collider;

    private void Awake()
    {
        try
        {
            collider = GetComponent<BoxCollider>();
        }
        catch(NullReferenceException ex)
        {
            Debug.LogException(ex);
        }
    }

    private void Start()
    {
        holeCompleted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GolfBall golfBall = other.GetComponent<GolfBall>();

        if(golfBall != null)
        {
            Debug.Log("Hit");
            celebrationParticle.Play();

            holeCompleted = true;
        }
    }
}
