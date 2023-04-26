using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBorderLevelLoader : MonoBehaviour
{
    [SerializeField]
    string levelName;

    [SerializeField]
    GameManager.SpawnSide spawnSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<GameManager>().BeginTraverseScenes(levelName);
            FindObjectOfType<GameManager>().SetSpawnSide(spawnSide);
        }
    }
}
