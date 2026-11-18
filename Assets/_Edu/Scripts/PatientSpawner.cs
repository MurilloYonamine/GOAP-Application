using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    public GameObject patientPrefab; // Prefab do paciente a ser instanciado.
    public int initialNumberOfPatients = 0; // Número inicial de pacientes a serem instanciados.
    void Start()
    {
        for (int i = 0; i < initialNumberOfPatients; i++)
        {
            // Instancia o paciente na posição do spawner.
            Instantiate(patientPrefab, transform.position, Quaternion.identity); 
        }
        // Agenda a chamada de SpawnPatient entre 3 e 8 segundos após o início do jogo.
        Invoke(nameof(SpawnPatient), Random.Range(3, 9));
    }
    private void SpawnPatient()
    {
        // Instancia um paciente na posição do spawner.
        Instantiate(patientPrefab, transform.position, Quaternion.identity); 

        // Repete a chamada de SpawnPatient entre 3 e 8 segundos.        
        Invoke(nameof(SpawnPatient), Random.Range(3, 9));
    }
}
