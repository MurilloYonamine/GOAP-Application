using UnityEngine;

public class GetPatient : GoapAction
{
    GameObject resource;
    public override bool PrePerform()
    {
        target = GoapWorld.Instance.GetNextPatientFromQueue();
        // Se não houver paciente na fila, a ação não pode ser realizada.
        if(target == null)
        {
            Debug.Log("Não há pacientes na fila.");
            return false;
        }

        resource = GoapWorld.Instance.GetNextTreatmentAreaFromQueue();

        if(resource != null)
        {
            inventory.AddItem(resource); // Adiciona a área de tratamento ao inventário do agente.
        }

        else
        {
            // Retorna o paciente à fila se não houver área de tratamento disponível.
            GoapWorld.Instance.AddPatientToQueue(target);
            // Limpa o alvo da ação, indicando que a ação não pode ser realizada.
            target = null;
            // A ação não pode ser realizada sem uma área de tratamento disponível. 
            return false; 
        }
        // Atualiza o estado do mundo, indicando que uma área de tratamento foi ocupada.
        GoapWorld.Instance.GetWorldStates().UpdateState("hasTreatmentArea", -1); 
        return true;
    }

    public override bool PostPerform()
    {
        // Atualiza o estado do mundo, indicando que o paciente não está mais na sala de espera.
        GoapWorld.Instance.GetWorldStates().UpdateState("atWaitingRoom", -1);

        if (target)
        {
            // Adiciona a área de tratamento ao inventário do paciente.
            target.GetComponent<GoapAgent>().inventory.AddItem(resource); 
        }
        
        return true;
    }
}
