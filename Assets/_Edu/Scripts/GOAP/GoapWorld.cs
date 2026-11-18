using System.Collections.Generic;
using UnityEngine;


namespace GOAP.EDU
{
    // Classe que representa o mundo do jogo, implementando o padrão Singleton.
    // Esta classe é responsável por armazenar e gerenciar os estados globais do mundo, 
    // bem como a fila de pacientes e áreas de atendimento.
    public sealed class GoapWorld
    {
        // Implementação do padrão Singleton para garantir que apenas uma 
        // instância da classe World exista durante a execução do programa.
        private static readonly GoapWorld instance = new GoapWorld();
        // Dicionario que armazena os estados do mundo.
        private static WorldStates worldStates;
        // Fila de pacientes
        private static Queue<GameObject> patientQueue;
        // Fila de áreas de atendimento
        private static Queue<GameObject> treatmentAreasQueue;
        // Inicializa a instancia dos dicionarios do mundo.
        static GoapWorld()
        {
            worldStates = new WorldStates();
            patientQueue = new Queue<GameObject>();
            treatmentAreasQueue = new Queue<GameObject>();
            // Popula a fila de area de tratamento com os objetos que possuem a tag "Cubicle" na cena.
            GameObject[] treatmentAreas = GameObject.FindGameObjectsWithTag("Cubicle");
            foreach (GameObject treatmentArea in treatmentAreas)
            {
                treatmentAreasQueue.Enqueue(treatmentArea);
            }
            if (treatmentAreasQueue.Count > 0)
            {
                worldStates.AddState("hasTreatmentArea", treatmentAreasQueue.Count);
            }
        }
        // Construtor privado para evitar a criação de instâncias adicionais da classe World.
        private GoapWorld() { }
        // Propriedade pública que fornece acesso à instância única da classe World.
        public static GoapWorld Instance
        {
            get
            {
                return instance;
            }
        }
        // Método público que retorna a instância dos estados do mundo.
        public WorldStates GetWorldStates()
        {
            return worldStates;
        }

        // Método público que adiciona um paciente à fila de pacientes.
        public void AddPatientToQueue(GameObject patient)
        {
            patientQueue.Enqueue(patient);
        }

        // Método público que retorna o próximo paciente da fila de pacientes.
        public GameObject GetNextPatientFromQueue()
        {
            if (patientQueue.Count > 0)
            {
                return patientQueue.Dequeue();
            }
            return null;
        }

        // Método público que adiciona uma área de atendimento à fila de áreas de atendimento.
        public void AddTreatmentAreaToQueue(GameObject treatmentArea)
        {
            treatmentAreasQueue.Enqueue(treatmentArea);
        }

        // Método público que retorna a próxima área de atendimento da fila de áreas de atendimento.
        public GameObject GetNextTreatmentAreaFromQueue()
        {
            if (treatmentAreasQueue.Count > 0)
            {
                return treatmentAreasQueue.Dequeue();
            }
            return null;
        }
    }
}