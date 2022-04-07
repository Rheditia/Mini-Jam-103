using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1f;
    Animator sceneTransitionAnimator;
    RemnantSpawner remnantSpawner;

    private void Awake()
    {
        remnantSpawner = FindObjectOfType<RemnantSpawner>();
        sceneTransitionAnimator = GetComponentInChildren<Animator>();
    }

    public void ChangeLevel()
    {
        //index = SceneManager.GetActiveScene().buildIndex;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            FindObjectOfType<AudioPlayer>().ResetAudio();
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        StartCoroutine(LoadScene(nextSceneIndex));
    }

    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(currentSceneIndex));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene(0));
    }

    public void LoadCredit()
    {
        StartCoroutine(LoadScene(1));
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene(2));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        sceneTransitionAnimator.SetTrigger("endScene");
        yield return new WaitForSeconds(sceneLoadDelay);
        if (remnantSpawner) { remnantSpawner.SpawnRemnant(); }
        SceneManager.LoadScene(sceneIndex);
    }
}
