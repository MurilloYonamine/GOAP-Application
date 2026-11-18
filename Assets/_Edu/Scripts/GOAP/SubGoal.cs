using System.Collections.Generic;

public class SubGoal
{
    public Dictionary<string, int> sgoals; // Dicionário que armazena os subobjetivos e seus valores.
    public bool remove; // Indica se o subobjetivo deve ser removido após ser alcançado.

    public SubGoal(string name, int cost, bool remove)
    {
        sgoals = new Dictionary<string, int>(); // Inicializa o dicionário de subobjetivos.
        sgoals.Add(name, cost); // Adiciona o subobjetivo ao dicionário.
        this.remove = remove; // Define se o subobjetivo deve ser removido após ser alcançado.
    }
}