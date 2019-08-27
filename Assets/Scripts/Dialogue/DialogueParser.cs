using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // csv 파일 가져옴

        // 엔터 간격으로 데이터 나누기
        string[] data = csvData.text.Split(new char[] { '\n' });

        // 데이터의 개수만큼 반복문 >> 마지막 증감식 사용x
        for(int i=1; i<data.Length;)
        {
            // 콤마로 구분하여 나누기
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue(); // 대사 리스트 생성

            dialogue.name = row[1];
            List<string> contextList = new List<string>();
            List<string> spriteList = new List<string>();
            List<string> voiceList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                spriteList.Add(row[3]);
                voiceList.Add(row[4]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                } else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();

            dialogue.spriteName = spriteList.ToArray();

            dialogue.voiceName = voiceList.ToArray();

            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }

}
