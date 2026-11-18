using UnityEngine;


namespace GOAP.EDU
{
    public class GetRest : GoapAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            agentStates.RemoveState("isTired");
            return true;
        }
    }
}