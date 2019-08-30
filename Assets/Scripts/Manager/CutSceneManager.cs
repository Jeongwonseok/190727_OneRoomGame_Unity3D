using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    // 컷씬 불러오기 종료 후 대사 불러올수 있도록!!
    public static bool isFinished = false;

    SplashManager theSplashManager;
    CameraController theCam;

    [SerializeField] Image img_CutScene;

    // Start is called before the first frame update
    void Start()
    {
        theSplashManager = FindObjectOfType<SplashManager>();
        theCam = FindObjectOfType<CameraController>();
    }

    public bool CheckCutScene()
    {
        return img_CutScene.gameObject.activeSelf;
    }

    public IEnumerator CutSceneCoroutine(string p_CutSceneName, bool p_isShow)
    {
        // 흰색 화면 나오도록!!
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeOut(true, false));
        yield return new WaitUntil(() => SplashManager.isFinished);

        if(p_isShow)
        {
            Sprite t_sprite = Resources.Load<Sprite>("CutScenes/" + p_CutSceneName);
            if (t_sprite != null)
            {
                img_CutScene.gameObject.SetActive(true);
                img_CutScene.sprite = t_sprite;
                theCam.CameraTargetting(null, 0.1f, true, false);
            }
            else
            {
                Debug.LogError("잘못된 컷신 CG 파일 이름입니다.");
            }
        }
        else
        {
            img_CutScene.gameObject.SetActive(false);
        }

        // 흰색 화면 사라지도록!!
        SplashManager.isFinished = false;
        StartCoroutine(theSplashManager.FadeIn(true, false));
        yield return new WaitUntil(() => SplashManager.isFinished);

        yield return new WaitForSeconds(0.5f);

        isFinished = true;
    }
}
