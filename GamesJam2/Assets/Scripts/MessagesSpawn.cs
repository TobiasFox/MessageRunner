using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesSpawn : MonoBehaviour
{
    [SerializeField]
    private int maxTryCounter;
    [SerializeField]
    private PoolBehaviour poolBehaviour;
    //[SerializeField]
    //[Tooltip("order: blue, green, red, yellow")]
    //private Color[] messageColors;
    [SerializeField]
    DefineColors customColors;

    private List<Vector3> spawnPositions;
    private Dictionary<Vector3, bool> isPositionBlocked;

    // Start is called before the first frame update
    void Start()
    {
        isPositionBlocked = new Dictionary<Vector3, bool>();
        spawnPositions = new List<Vector3>();
        
        foreach(Transform child in transform)
        {
            if(child!=transform)
            {
                spawnPositions.Add(child.position);
                isPositionBlocked.Add(child.position, false);
            }
            
        }
        for(int i=0;i< System.Enum.GetValues(typeof(DefineColors.Colors)).Length;i++)
        {
            SpawnMessage((DefineColors.Colors)i);
        }

    }

    public void SpawnMessage(DefineColors.Colors color)
    {
        GameObject newMessage=poolBehaviour.GetObject();
        Renderer rend = newMessage.GetComponent<Renderer>();
        //rend.material.color = messageColors[(int)color];
        rend.material.color = customColors.colors[(int)color];
        newMessage.transform.position = GetFreePosition();
        BlockPosition(newMessage.transform.position);
    }

    private Vector3 GetFreePosition()
    {
        int randIndex = 0;
        int tryCounter = 0;
        do
        {
            randIndex = Random.Range(0, spawnPositions.Count);
            tryCounter++;
        } while (isPositionBlocked[spawnPositions[randIndex]] && tryCounter<maxTryCounter);

        return spawnPositions[randIndex];
    }

    private void BlockPosition(Vector3 position)
    {
        if (isPositionBlocked.ContainsKey(position))
            isPositionBlocked[position] = true;
        else
            Debug.Log("doesn't contain key");
    }
    private void FreePosition(Vector3 position)
    {
        if (isPositionBlocked.ContainsKey(position))
            isPositionBlocked[position] = false;
        else
            Debug.Log("doesn't contain key");
    }
}
