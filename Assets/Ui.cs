using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    Camera mainCamera;
    public Camera playerCamera;
    public GameObject player;
    Scene currentScene;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame() {
        transform.GetComponent<Canvas>().enabled = false;
        mainCamera.enabled = false;
        player.SetActive(true);

            }
    IEnumerator nextScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene.name+1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
