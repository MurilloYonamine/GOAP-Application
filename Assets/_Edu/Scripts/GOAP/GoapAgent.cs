using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GOAP.EDU
{
    public class GoapAgent : MonoBehaviour
    {
        // Lista de ações disponíveis para o agente.
        public List<GoapAction> actions = new List<GoapAction>();
        // Dicionário de subobjetivos e suas prioridades.
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        // Ação atualmente em execução pelo agente.
        public GoapAction currentAction;
        // Fila de ações planejadas para alcançar o objetivo atual.
        public Queue<GoapAction> actionQueue;
        // Objetivo atual do agente.
        public SubGoal currentGoal;
        // Planejador de ações do agente.
        public GoapPlanner planner;

        // Estados internos do agente, representando informações sobre o mundo e o próprio agente.
        public WorldStates personalStates = new WorldStates();
        // Inventário do agente
        public GoapInventory inventory = new GoapInventory();
        bool invoked = false; // Flag para controlar a invocação de métodos repetitivos.
        float distanceToTargetOffset = 3f; // Distância mínima para considerar que o agente chegou ao alvo da ação.

        protected virtual void Start()
        {
            GoapAction[] availableActions = GetComponents<GoapAction>(); // Obtém todas as ações anexadas ao agente.
            foreach (GoapAction a in availableActions)
            {
                actions.Add(a); // Adiciona cada ação à lista de ações do agente.
            }
        }

        // Método que indica se o agente completou todas as ações planejadas.
        void CompleteAction()
        {
            currentAction.performAction = false; // Marca a ação atual como não em execução.
            currentAction.PostPerform(); // Executa qualquer lógica pós-execução da ação.
            invoked = false; // Reseta a flag de invocação.
        }

        void Update()
        {
            // Se está executando uma ação, verifica se chegou ao alvo da ação para completar a execução.
            if (currentAction != null && currentAction.performAction)
            {
                Debug.Log($"O agente {gameObject.name} está executando a ação {currentAction.actionName}.");
                float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, transform.position); // Calcula a distância até o alvo da ação.

                if (currentAction.agent.hasPath && distanceToTarget < distanceToTargetOffset)
                {
                    if (!invoked)
                    {
                        // Invoca o método CompleteAction após a duração da ação.
                        Invoke("CompleteAction", currentAction.duration);
                        invoked = true; // Marca que a ação foi invocada.
                    }
                }
                // Se o agente não estiver no destino correto, atualiza o destino do agente para o alvo da ação.
                else if (currentAction.agent.destination != currentAction.target.transform.position)
                {
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
                return;
            }
            if (planner == null || actionQueue == null)
            {
                Debug.Log($"O agente {gameObject.name} está tentando criar um novo plano.");
                // Se não houver um planejador ou fila de ações, tenta criar um novo plano.
                planner = new GoapPlanner();
                // Ordena os objetivos por prioridade.
                var sortedGoals = from entry in goals orderby entry.Value descending select entry;
                // Outra variação de ordenação com Linq seria: 
                // var sortedGoals = goals.OrderByDescending(x => x.Value);
                foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    // Tenta criar um plano para cada objetivo até encontrar um que seja alcançável.
                    actionQueue = planner.ActionPlan(actions, sg.Key.sgoals, personalStates);
                    if (actionQueue != null)
                    {
                        currentGoal = sg.Key; // Define o objetivo atual.
                        break; // Sai do loop se um plano válido for encontrado.
                    }
                }
            }

            // Se a fila de ações não for nula, mas estiver vazia, significa que o objetivo foi alcançado.
            if (actionQueue != null && actionQueue.Count == 0)
            {
                Debug.Log($"O objetivo {currentGoal} foi alcançado.");
                // Se o objetivo pode ser removido.
                if (currentGoal.remove)
                {
                    goals.Remove(currentGoal); // Remove o objetivo.
                }
                planner = null; // Reseta o planejador atual.
            }

            if (actionQueue != null && actionQueue.Count > 0)
            {
                // Se houver ações na fila, pega a próxima ação para execução.
                currentAction = actionQueue.Dequeue();
                Debug.Log($"A ação {currentAction.actionName} foi retirada da fila para execução.");
                if (currentAction.PrePerform())
                {
                    if (currentAction.target == null && currentAction.targetTag != "")
                    {
                        // Se a ação não tem um alvo definido, procura um alvo com a tag especificada.
                        GameObject target = GameObject.FindWithTag(currentAction.targetTag);
                        if (target != null)
                        {
                            currentAction.target = target; // Define o alvo da ação.
                        }
                    }
                    if (currentAction.target != null)
                    {
                        // Se a ação tem um alvo definido, marca a ação como em execução.
                        currentAction.performAction = true;
                        currentAction.agent.SetDestination(currentAction.target.transform.position); // Define o destino do agente para o alvo da ação.
                    }
                }
                else
                {
                    actionQueue = null; // Se a ação não puder ser executada, reseta a fila de ações.
                }
            }
        }
    }
}