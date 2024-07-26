using Golf;

using System.Collections.Generic;

using UnityEngine;

namespace Managers
{
    public class CourseManager : MonoBehaviour
    {
        public int CurrentHole { get; set; }
        public int CurrentPar { get; set; }

        public Dictionary<int, Hole> courses;
    }
}
