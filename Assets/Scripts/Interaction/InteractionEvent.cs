using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;

    public Dialogue[] GetDialogue()
    {
        DialogueEvent t_DialogueEvent = new DialogueEvent();
        t_DialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            t_DialogueEvent.dialogues[i].tf_Target = dialogueEvent.dialogues[i].tf_Target;
        }

        dialogueEvent.dialogues = t_DialogueEvent.dialogues;

        return dialogueEvent.dialogues;
    }
}
