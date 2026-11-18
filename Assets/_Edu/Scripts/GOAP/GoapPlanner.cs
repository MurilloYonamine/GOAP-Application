using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoapPlanner
{
    public Queue<GoapAction> ActionPlan(List<GoapAction> actions, Dictionary<string, int> goal, WorldStates worldStates)
    {
        // Implementação do algoritmo de planejamento (GOAP) para gerar uma sequência de ações
        // que levam ao objetivo especificado, considerando os estados do mundo.
        // Retorna uma fila de ações a serem executadas para alcançar o objetivo.
        List<GoapAction> usableActions = new List<GoapAction>();
        foreach (GoapAction a in actions)
        {
            if (a.ActionIsAchievable())
            {
                usableActions.Add(a);
            }
        }

        List<GoapNode> leaves = new List<GoapNode>();
        // Cria o nó inicial com os estados do mundo e os estados do agente.
        GoapNode start = new GoapNode(null, 0, GoapWorld.Instance.GetWorldStates().GetStates(), worldStates.GetStates(), null);
        bool success = BuildGraph(start, leaves, usableActions, goal);
        if(!success)
        {
            Debug.Log("Não foi possível encontrar um plano para o objetivo.");
            return null;
        }

        // Encontra o nó folha com o menor custo (caminho mais barato) entre os nós folhas encontrados.
        GoapNode cheapest = null;
        foreach(GoapNode leaf in leaves)
        {
            if(cheapest == null)
            {
                cheapest = leaf;
            }
            else if(leaf.cost < cheapest.cost)                            
            {
                cheapest = leaf;
            }            
        }

        // Constrói a sequência de ações a partir do nó folha mais barato até o nó raiz, 
        // adicionando as ações à fila de execução.
        List<GoapAction> result = new List<GoapAction>();
        GoapNode n = cheapest;
        while(n != null)
        {
            if(n.action != null)
            {
                // Adiciona ao final, mais eficiente
                result.Add(n.action);
            }
            n = n.parent;
        }
        // Inverte a lista de ações para que a primeira ação seja a primeira a ser executada.
        Queue<GoapAction> queue = new Queue<GoapAction>(result.AsEnumerable().Reverse());

        // Debug para exibir a sequência de ações planejadas no console.
        Debug.Log("Plano encontrado:");
        foreach(GoapAction a in queue)
        {
            Debug.Log("Fila - " + a.actionName);
        }
        return queue;
    }

    bool BuildGraph(GoapNode parent, List<GoapNode> leaves, List<GoapAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach(GoapAction action in usableActions)
        {
            if(action.ActionIsAchievableGiven(parent.state))
            {
                // Cria uma cópia dos estados do mundo e aplica os efeitos da ação.
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach(KeyValuePair<string, int> effect in action.effectsDict)
                {
                    if(!currentState.ContainsKey(effect.Key))
                    {
                        currentState.Add(effect.Key, effect.Value);
                    }                    
                }

                // Cria um novo nó filho com os estados atualizados e o custo acumulado.
                GoapNode node = new GoapNode(parent, parent.cost + action.cost, currentState, action);

                // Verifica se o objetivo foi alcançado com os estados atuais.
                if(GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    // Cria uma lista de ações restantes excluindo a ação atual.
                    List<GoapAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if(found)
                    {
                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach(KeyValuePair<string, int> g in goal)
        {
            if(!state.ContainsKey(g.Key))
            {
                return false;
            }
        }
        return true;
    }

    List<GoapAction> ActionSubset(List<GoapAction> actions, GoapAction removeMe)
    {
        List<GoapAction> subset = new List<GoapAction>();
        foreach(GoapAction a in actions)
        {
            if(!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }
        return subset;
    }
}
