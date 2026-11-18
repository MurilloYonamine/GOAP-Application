using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GOAP.EDU
{
    public class UpdateWorld : MonoBehaviour
    {
        public TextMeshProUGUI states;
        void Update()
        {
            Dictionary<string, int> wStates = GoapWorld.Instance.GetWorldStates().GetStates();
            states.text = "";
            foreach (KeyValuePair<string, int> s in wStates)
            {
                states.text += s.Key + ": " + s.Value + "\n";
            }
        }
    }
}