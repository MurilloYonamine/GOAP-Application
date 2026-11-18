using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace GOAP.EDU
{
    // Classe abstrata que representa uma ação no sistema GOAP (Goal-Oriented Action Planning).
    public abstract class GoapAction : MonoBehaviour
    {
        public string actionName = "Action"; // Nome da ação.
        public float cost = 1f; // Custo da ação.
        public float duration = 1f; // Duração da ação.
        public string targetTag = ""; // Tag do alvo da ação.
        public bool performAction = false; // Flag para indicar se a ação deve ser executada.

        // Referências do alvo da ação.
        public GameObject target; // Referência ao alvo da ação.
        public NavMeshAgent agent; // Referência ao agente de navegação.

        // Referências aos estados do mundo.
        public WorldState[] preconditions; // Estados do mundo necessários para executar a ação.
        public WorldState[] effects; // Efeitos da ação nos estados do mundo.

        // Dicionarios para facilitar a manipulação dos estados do mundo.
        public Dictionary<string, int> preconditionsDict; // Dicionário de pré-condições.
        public Dictionary<string, int> effectsDict; // Dicionário de efeitos.

        public WorldStates agentStates; // Referência aos estados do agente (internos).

        public GoapInventory inventory; // Referência ao inventário do agente.

        // Construtor que inicializa os dicionários de pré-condições e efeitos.
        public GoapAction()
        {
            preconditionsDict = new Dictionary<string, int>();
            effectsDict = new Dictionary<string, int>();
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>(); // Obtém a referência ao componente NavMeshAgent.
            if (preconditions != null)
            {
                foreach (WorldState state in preconditions)
                {
                    preconditionsDict.Add(state.key, state.value); // Adiciona as pré-condições ao dicionário.
                }
            }
            if (effects != null)
            {
                foreach (WorldState state in effects)
                {
                    effectsDict.Add(state.key, state.value); // Adiciona os efeitos ao dicionário.
                }
            }
            inventory = GetComponent<GoapAgent>().inventory; // Obtém a referência ao inventário do agente.
            agentStates = GetComponent<GoapAgent>().personalStates; // Obtém a referência aos estados do agente.
        }

        // Método placeholder para verificar se a ação é alcançável.
        public bool ActionIsAchievable() => true;

        public bool ActionIsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> precondition in preconditionsDict)
            {
                if (!conditions.ContainsKey(precondition.Key))
                {
                    return false; // Retorna falso se alguma pré-condição não for atendida.
                }
            }
            return true; // Retorna verdadeiro se todas as pré-condições forem atendidas.
        }

        public abstract bool PrePerform(); // Método abstrato para executar ações antes da ação principal.
        public abstract bool PostPerform(); // Método abstrato para executar ações após a ação principal.
    }
}