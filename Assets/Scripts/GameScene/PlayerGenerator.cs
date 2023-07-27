using UnityEngine;
using UnityEngine.Serialization;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    
    public void Start()
    {
        GeneratePlayer();
    }

    private void GeneratePlayer()
    {
        Instantiate(playerData.prefab, new Vector3(0, 1, 0), Quaternion.identity);
    }
}