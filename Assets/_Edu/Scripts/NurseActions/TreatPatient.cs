namespace GOAP.EDU
{
    public class TreatPatient : GoapAction
    {
        public override bool PrePerform()
        {
            // Verifica se há uma área de tratamento disponível no inventário do agente.
            target = inventory.FindItemWithTag("Cubicle");
            GoapWorld.Instance.GetWorldStates().UpdateState("TreatingPatient", 1);
            return target == null ? false : true;
        }

        public override bool PostPerform()
        {
            // Atualiza o estado do mundo, indicando que o paciente foi tratado.
            GoapWorld.Instance.GetWorldStates().UpdateState("TreatingPatient", -1);
            GoapWorld.Instance.AddTreatmentAreaToQueue(target);
            inventory.RemoveItem(target);
            GoapWorld.Instance.GetWorldStates().UpdateState("hasTreatmentArea", 1);
            return true;
        }
    }
}