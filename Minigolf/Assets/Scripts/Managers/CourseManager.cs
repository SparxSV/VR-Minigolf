using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    [SerializeField] private List<Hole> holes;

    private int totalHoles;

    private void Start()
    {
        totalHoles = holes.Count;
    }
}
