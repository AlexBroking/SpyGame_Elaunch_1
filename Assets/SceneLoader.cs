using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject loadingHead;
    private GameObject loadingBar;
    public SpriteRenderer backround;

    public bool loadScene = false;
    public bool doneLoading = false;

    
    public int scene;
    private float alphaColor = 0;
    private float loadingValue = 0;

    private void Start()
    {
        loadingHead = GameObject.Find("LoadingHead");
        loadingBar = loadingHead.transform.GetChild(0).gameObject;
        loadingHead.transform.GetChild(0).gameObject.SetActive(false);
        backround = loadingHead.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (loadScene == true)
        {

            alphaColor = Mathf.Lerp(alphaColor, 255, Time.deltaTime * 2.1f);
            backround.color = new Color32(255, 255, 255, (byte)alphaColor);
            
            if (alphaColor >= 250)
            {
                backround.color = new Color32(255, 255, 255, 255);
                loadingHead.transform.GetChild(0).gameObject.SetActive(true);
                loadScene = false;
                StartCoroutine(LoadNewScene());
            }
        }
    }


    IEnumerator LoadNewScene()
    {
        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        while (loadingValue <= 60f)
        {
            loadingValue = loadingValue + 0.4f;
            yield return new WaitForSeconds(0.005f);

            loadingBar.GetComponent<Slider>().value = loadingValue / 100;
        }

        while (!async.isDone && loadingValue >= 60f)
        {
            loadingBar.GetComponent<Slider>().value = async.progress;
            yield return new WaitForSeconds(2f);
            loadingBar.GetComponent<Slider>().value = 1;
            async.allowSceneActivation = true;
            yield return new WaitForSeconds(2f);

            yield return null;
        }
    }


}
