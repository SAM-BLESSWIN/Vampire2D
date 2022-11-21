using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //singleton design pattern
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public string[] levelNames;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Multiple Instances of singleton : Destroyed  "+gameObject.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(GetLevelStatus(levelNames[0]) == LevelStatus.LOCKED)
        {
            SetLevelStatus(levelNames[0], LevelStatus.UNLOCKED);
        }
    }

    public LevelStatus GetLevelStatus(string levelName)
    {
        return (LevelStatus)PlayerPrefs.GetInt(levelName,0);
    }

    public void SetLevelStatus(string levelName,LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(levelName,(int)levelStatus);
    }

    public int GetCurrentLevel()
    {
        return SceneController.GetCurrentSceneIndex();
    }

    public void MarkCurrentLevelComplete()
    {
        SetLevelStatus(SceneController.GetCurrentSceneName(), LevelStatus.COMPLETED);
        Debug.Log(SceneController.GetCurrentSceneName() + " completed");
        UnlockNextLevel();
    }

    private void UnlockNextLevel()
    {
        int currentLevelIndex = Array.FindIndex(levelNames,x=>x==SceneController.GetCurrentSceneName());
        int nextLevelIndex = currentLevelIndex + 1;
        if(nextLevelIndex < levelNames.Length)
        {
            if (GetLevelStatus(levelNames[nextLevelIndex]) == LevelStatus.LOCKED)
            {
                SetLevelStatus(levelNames[nextLevelIndex], LevelStatus.UNLOCKED);
                Debug.Log(levelNames[nextLevelIndex] + " unlocked");
            }
        }
    }
}
