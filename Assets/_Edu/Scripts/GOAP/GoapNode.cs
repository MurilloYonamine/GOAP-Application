using System.Collections.Generic;


namespace GOAP.EDU
{
    public class GoapNode
    {
        public GoapNode parent;
        public float cost;
        public Dictionary<string, int> state;
        public GoapAction action;

        public GoapNode(GoapNode parent, float cost, Dictionary<string, int> allStates, GoapAction action)
        {
            this.parent = parent;
            this.cost = cost;
            state = new Dictionary<string, int>(allStates);
            this.action = action;
        }

        public GoapNode(GoapNode parent, float cost, Dictionary<string, int> allStates, Dictionary<string, int> agentStates, GoapAction action)
        {
            this.parent = parent;
            this.cost = cost;
            state = new Dictionary<string, int>(allStates);
            foreach (KeyValuePair<string, int> aState in agentStates)
            {
                if (!state.ContainsKey(aState.Key))
                {
                    state.Add(aState.Key, aState.Value);
                }
            }
            this.action = action;
        }
    }
}