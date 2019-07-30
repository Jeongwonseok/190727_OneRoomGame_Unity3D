using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracrionController : MonoBehaviour
{
    [SerializeField] Camera cam;

    RaycastHit hitInfo;

    [SerializeField] GameObject go_NormalCrosshair;
    [SerializeField] GameObject go_InteractiveCrosshair;

    bool isContact = false;
    public static bool isInteract = false;

    [SerializeField] ParticleSystem ps_QuestionEffect;

    DialogueManager theDM;

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
        ClickLeftBtn();
    }

    void CheckObject()
    {
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        if(Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 100))
        {
            Contact();
        }
        else
        {
            NotContact();
        }
    }

    void Contact()
    {
        if(!isInteract)
        {
            if (hitInfo.transform.CompareTag("Interaction"))
            {
                if (!isContact)
                {
                    isContact = true;
                    go_InteractiveCrosshair.SetActive(true);
                    go_NormalCrosshair.SetActive(false);
                }

            }
            else
            {
                NotContact();
            }
        }
        
    }

    void NotContact()
    {
        if(isContact)
        {
            isContact = false;
            go_InteractiveCrosshair.SetActive(false);
            go_NormalCrosshair.SetActive(true);
        }
    }

    void ClickLeftBtn()
    {
        if(!isInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isContact)
                {
                    Interact();
                }
            }
        }

    }

    void Interact()
    {
        isInteract = true;

        ps_QuestionEffect.gameObject.SetActive(true);
        Vector3 t_targetPos = hitInfo.transform.position;
        ps_QuestionEffect.GetComponent<QuestionEffect>().SetTarget(t_targetPos);
        ps_QuestionEffect.transform.position = cam.transform.position;

        StartCoroutine(WaitCollision());
    }

    IEnumerator WaitCollision()
    {
        yield return new WaitUntil(() => QuestionEffect.isCollide);
        QuestionEffect.isCollide = false;

        theDM.ShowDialogue();
    }
}
