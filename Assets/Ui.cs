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
        Time.timeScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame() {
        transform.GetComponent<Canvas>().enabled = false;
        mainCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera.enabled = true;
        Time.timeScale = 1;
        //player.SetActive(true);

    }
    public void die()
    {
        StartCoroutine(nextScene());
    }
    IEnumerator nextScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene.name);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
