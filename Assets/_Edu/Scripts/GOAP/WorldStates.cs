using System.Collections.Generic;

// Essa classe representa um conjunto de estados do mundo, permitindo 
// a manipulação de estados através de métodos para adicionar, remover, 
// atualizar e definir valores de estados.
public class WorldStates
{
    public Dictionary<string, int> states;
    // Método construtor que inicializa o dicionário de estados do mundo.
    public WorldStates()
    {
        states = new Dictionary<string, int>();
    }

    // Verifica se o estado do mundo com a chave especificada existe no dicionário.
    public bool HasState(string key)
    {
        return states.ContainsKey(key);
    }

    // Adiciona um novo estado do mundo ao dicionário com a chave e valor especificados.
    public void AddState(string key, int value)
    {
        states[key] = value;
    }

    // Remove o estado do mundo com a chave especificada do dicionário.
    public void RemoveState(string key)
    {
        if (states.ContainsKey(key))
        {
            states.Remove(key);
        }
    }

    // Atualiza o valor do estado do mundo com a chave especificada.
    public void UpdateState(string key, int value)
    {
        // Se o estado já existe, atualiza o valor somando o valor fornecido. 
        if (states.ContainsKey(key))
        {
            states[key] += value;

            // Se o valor resultante for menor ou igual a zero, remove o estado do dicionário.
            if(states[key] <= 0)
            {
                RemoveState(key);
            }
        }
        // Se o estado ainda nao existe, podemos adicionar-lo com o valor fornecido.
        else
        {
            AddState(key, value);
        }
    }

    // Define o valor do estado do mundo com a chave especificada sem incrementar ou decrementar o valor existente.
    public void SetState(string key, int value)
    {
        if(states.ContainsKey(key))
        {
            states[key] = value;
        }
        else
        {
            AddState(key, value);
        }
    }

    // Retorna o dicionário de estados do mundo.
    public Dictionary<string, int> GetStates()
    {        
        return states;
    }
}