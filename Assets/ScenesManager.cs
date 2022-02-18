using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private const string UiSceneName = "UiScene";

    private string currentSceneName;

    public static ScenesManager Instance;

    private int currentSceneIndex = 0;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        
        SceneManager.LoadScene(UiSceneName, LoadSceneMode.Additive);

        this.currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.UnloadScene(this.currentSceneIndex);
            SceneManager.LoadScene(this.currentSceneIndex);
        }
        else if(Input.GetKeyDown(KeyCode.N))
        {
           this.currentSceneIndex++;
           if(this.currentSceneIndex > 2)
            {
                this.currentSceneIndex = 2;
            }
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.UnloadScene(activeScene);
            SceneManager.LoadScene(this.currentSceneIndex);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            this.currentSceneIndex--;
            if (this.currentSceneIndex < 0)
            {
                this.currentSceneIndex = 0;
            }
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.UnloadScene(activeScene);
            SceneManager.LoadScene(this.currentSceneIndex);

            this.currentSceneName = SceneManager.GetActiveScene().name;
        }
    }

}
