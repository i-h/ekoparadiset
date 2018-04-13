using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogTools : MonoBehaviour {
    public static DialogStruct ParseDialogue(string content)
    {
        DialogStruct d = new DialogStruct();

        StringReader sr = new StringReader(content);
        d.Content = sr.ReadLine();
        d.Exits = new List<DialogExit>();
        string input;
        while ((input = sr.ReadLine()) != null)
        {
            DialogExit e = new DialogExit();
            string[] line = input.Split('§');
            e.Prompt = line[0].Trim();
            if (e.Prompt.Length > 0)
            {
                switch (e.Prompt[0])
                {
                    case '~':
                        d.Target = e.Prompt.Substring(1);
                        continue;
                    case '*':
                        e.Points = 1;
                        e.Prompt = e.Prompt.Substring(1);
                        break;
                    default:

                        break;
                }
            }
            if (d.Target != null && d.Target.Length > 0)
            {
                e.Target = d.Target;
            }
            else if (line.Length > 1)
            {
                e.Target = line[1].Trim();
            }
            else
            {
                e.Target = "EXIT";
            }
            d.Exits.Add(e);
        }
        sr.Close();

        return d;
    }
}

[System.Serializable]
public struct DialogStruct
{
    public string Content;
    public string Target;
    public List<DialogExit> Exits;
}
[System.Serializable]
public struct DialogExit
{
    public string Prompt;
    public string Target;
    public int Points;
}
