using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [HideInInspector]
    public int sheepSaved;

    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver;
    public SheepSpawner sheepSpawner;

    public int sheepToNextLevel = 10;
    public string nextSceneName = "Level2";
    public bool advanceLevels = true;

    public SceneFader sceneFader;

    void Awake()
    {
        Instance = this;
    }

    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();

        if (advanceLevels && sheepSaved >= sheepToNextLevel)
        {
            sceneFader.FadeAndLoad(nextSceneName);
        }
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();
    }

    public void DroppedSheep()
    {
        sheepDropped++;

        if (sheepDropped == sheepDroppedBeforeGameOver)
        {
            GameOver();
        }

        UIManager.Instance.UpdateSheepDropped();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }
}