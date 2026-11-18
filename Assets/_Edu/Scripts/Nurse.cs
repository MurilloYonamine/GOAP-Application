using UnityEngine;


namespace GOAP.EDU
{
    public class Nurse : GoapAgent
    {
        protected override void Start()
        {
            base.Start();
            // Objetivo da enfermeira é atender pacientes, com prioridade 3 (maior), 
            // e não será removido após ser alcançado.
            SubGoal s1 = new SubGoal("patientTreated", 1, false);
            goals.Add(s1, 3);

            // Segundo objetivo - descansar
            SubGoal s2 = new SubGoal("isResting", 1, false);
            // Prioridade maior se está cansada.
            goals.Add(s2, 5);

            Invoke(nameof(GetTired), Random.Range(10, 20));
        }

        void GetTired()
        {
            personalStates.UpdateState("isTired", 0);
            Invoke(nameof(GetTired), Random.Range(10, 20)); // Chama GetTired a cada 10 a 20 segundos.
        }
    }
}