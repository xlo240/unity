using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    AsyncOperation AsyncOperation;
    int SceneID = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartBtn(){
        StartCoroutine(LoadSceneCor());
        Debug.Log("FFFFFFFFF");
    }
    IEnumerator LoadSceneCor(){
        yield return new WaitForSeconds(0.5f);
        AsyncOperation = SceneManager.LoadSceneAsync(SceneID);
    }
}
