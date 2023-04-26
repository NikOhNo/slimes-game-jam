using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    SpawnSide spawnSide = SpawnSide.left;

    [SerializeField]
    Transform leftSpawn;

    [SerializeField]
    Transform rightSpawn;

    [SerializeField]
    bool hungerMode = false;

    [SerializeField]
    float fadeTime = 0.5f;
    
    CanvasGroup FadePanel;

    public enum SpawnSide
    {
        left,
        right
    }

    private void Awake()
    {
        int singletonCount = FindObjectsOfType<GameManager>().Length;
        if (singletonCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            FadePanel = GetComponentInChildren<CanvasGroup>();
            FadePanel.alpha = 0;
        }
    }

    public void SetSpawnSide(SpawnSide side)
    {
        spawnSide = side;
    }

    public void BeginTraverseScenes(string nextScene)
    {
        StartCoroutine(TraverseScenes(nextScene));
    }

    IEnumerator TraverseScenes(string nextScene)
    {
        // Disable Movement
        player.GetComponent<PlayerMovement>().SetCanMove(false);

        // Fade in
        yield return StartCoroutine(FadeScreen(0, 1));

        // Set correct spawn
        if (spawnSide == SpawnSide.right)
        {
            player.transform.position = rightSpawn.position;
        }    
        else
        {
            player.transform.position = leftSpawn.position;
        }
        
        // Load next scene
        FindObjectOfType<SceneLoader>().LoadSceneName(nextScene);

        // Fade out
        yield return StartCoroutine(FadeScreen(1, 0));

        // Reenable movement
        player.GetComponent<PlayerMovement>().SetCanMove(true);
    }

    IEnumerator FadeScreen(float startVal, float endVal)
    {
        bool fadeComplete = false;
        float timeElapsed = 0f;

        FadePanel.alpha = startVal;
        while (!fadeComplete)
        {
            FadePanel.alpha = Mathf.Lerp(startVal, endVal, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= fadeTime)
            {
                fadeComplete = true;
            }
            yield return null;
        }
        FadePanel.alpha = endVal;
    }

    public void ActivateHunger()
    {
        throw new NotImplementedException();
    }

    public bool GetHungerMode()
    {
        return hungerMode;
    }
}
