using System.Collections.Generic;
using UnityEngine;

// Classe que representa o inventário do agente, permitindo armazenar e 
// gerenciar itens coletados durante a execução das ações.
// Aceita qualquer tipo de GameObject.
public class GoapInventory
{
    // Lista de itens no inventário do agente.
    List<GameObject> items = new List<GameObject>();

    // Adiciona um item ao inventário do agente.
    public void AddItem(GameObject item)
    {
        items.Add(item);
    }

    // Verifica se o inventário do agente contém um item com a tag especificada.
    public GameObject FindItemWithTag(string tag)
    {
        foreach (GameObject item in items)
        {
            if (item.CompareTag(tag))
            {
                return item;
            }
        }
        return null;
    }

    // Remove um item do inventário do agente.
    public void RemoveItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }
}