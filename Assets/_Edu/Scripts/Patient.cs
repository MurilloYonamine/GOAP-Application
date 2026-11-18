public class Patient : GoapAgent
{    
    protected override void Start()
    {
        base.Start();
        // Objetivo do paciente é esperar, com prioridade 1, 
        // e será removido após ser alcançado.
        SubGoal s1 = new SubGoal("isWaiting", 1, true);
        goals.Add(s1, 3);

        // É mais importante ser tratado do que esperar, então tem 
        // prioridade 5, e será removido após ser alcançado.
        SubGoal s2 = new SubGoal("isTreated", 1, true);
        goals.Add(s2, 5);

        // Objetivo do paciente é ir para casa, com prioridade 1,
        SubGoal s3 = new SubGoal("atHome", 1, true);
        goals.Add(s3, 1);
    }
}
