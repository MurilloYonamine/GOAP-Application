
namespace GOAP.EDU
{
    public class GoToWaitingRoom : GoapAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            GoapWorld.Instance.GetWorldStates().UpdateState("atWaitingRoom", 1);
            GoapWorld.Instance.AddPatientToQueue(gameObject);
            agentStates.UpdateState("atHospital", 1);
            return true;
        }
    }
}