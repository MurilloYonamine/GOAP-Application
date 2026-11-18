// Essa estrutura permite que os valores key e value sejam serializados e exibidos no Inspector da Unity.
// São utilizadas na criação de dicionários de estados do mundo.


namespace GOAP.EDU
{
    [System.Serializable]
    public class WorldState
    {
        public string key;
        public int value;
    }
}