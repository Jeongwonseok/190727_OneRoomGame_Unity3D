using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;

    Dialogue[] dialogues;

    bool isDialogue = false;  // 대화중일때 true
    bool isNext = false;      // 특정 키 입력 대기

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;  // 대화 카운트
    int contextCount = 0; // 대사 카운트

    InteracrionController theIC;
    CameraController theCam;

    void Start()
    {
        theIC = FindObjectOfType<InteracrionController>();
        theCam = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        if(isDialogue)
        {
            if(isNext)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount < dialogues.Length)
                        {
                            CameraTargettingType();
                        }
                        else
                        {
                            EndDialogue();

                        }
                    }
                }
            }
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        theIC.SettingUI(false);

        dialogues = p_dialogues;
        theCam.CamOriginSetting();

        CameraTargettingType();
    }

    void CameraTargettingType()
    {
        switch(dialogues[lineCount].cameraType)
        {
            case CameraType.ObjectFront: theCam.CameraTargetting(dialogues[lineCount].tf_Target); break;
            case CameraType.Reset: theCam.CameraTargetting(null, 0.05f, true, false); break;
        }
        StartCoroutine(TypeWriter());
    }

    void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;

        theCam.CameraTargetting(null, 0.05f, true, true);

        SettingUI(false);
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");


        bool t_white = false, t_yellow = false, t_cyan = false;
        bool t_ignore = false;

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch(t_ReplaceText[i])
            {
                case 'ⓦ': t_white = true; t_yellow = false; t_cyan = false; t_ignore = true; break;
                case 'ⓨ': t_white = false; t_yellow = true; t_cyan = false; t_ignore = true; break;
                case 'ⓒ': t_white = false; t_yellow = false; t_cyan = true; t_ignore = true; break;
            }

            string t_letter = t_ReplaceText[i].ToString();

            if(!t_ignore)
            {
                if(t_white)
                    t_letter = "<color=#ffffff>" + t_letter + "</color>";
                else if (t_yellow)
                    t_letter = "<color=#ffff00>" + t_letter + "</color>";
                else if (t_cyan)
                    t_letter = "<color=#42DEE3>" + t_letter + "</color>";
                txt_Dialogue.text += t_letter;
            }
            t_ignore = false;


            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;

    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);

        if(p_flag)
        {
            if(dialogues[lineCount].name == "")
            {
                go_DialogueNameBar.SetActive(false);
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }
        }
        else
        {
            go_DialogueNameBar.SetActive(false);
        }

    }
}
