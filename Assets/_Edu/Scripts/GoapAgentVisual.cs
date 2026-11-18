using UnityEngine;


namespace GOAP.EDU
{
    [ExecuteInEditMode]
    public class GoapAgentVisual : MonoBehaviour
    {
        public GoapAgent thisAgent;

        void Start()
        {
            thisAgent = GetComponent<GoapAgent>();
        }
    }
}