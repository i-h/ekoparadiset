 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Dialogue))]
public class DialogueTreeEditor : Editor
{
    string[] _dialogues;
    int _selected = 0;
    int _prevSelected = 0;
    string _content = "";
    string _dialogDestination = "";
    DialogStruct _selectedDialog;
    public override void OnInspectorGUI()
    {
        DirectoryInfo info = new DirectoryInfo("Assets/Resources/Dialogues");
        FileInfo[] files = info.GetFiles("*.txt");
        if (_dialogues == null) {
            _dialogues = new string[files.Length+1];
            _dialogues[0] = "Custom";
            for(int i = 0; i < files.Length; i++)
            {
                _dialogues[i+1] = files[i].Name;
            }
        }

        _prevSelected = _selected;
        _selected = EditorGUILayout.Popup(_prevSelected, _dialogues);
        if(_prevSelected != _selected)
        {
            if (_selected == 0)
            {
                _content = "";
            }
            else
            {
                _selectedDialog = ParseDialogue(files[_selected - 1].FullName);
                ((Dialogue)target).Content = _selectedDialog.Content;
                ((Dialogue)target).Exits = _selectedDialog.Exits.ToArray();
                if(_selectedDialog.Target.Length > 0) ((Dialogue)target).Target = _selectedDialog.Target;
                ((Dialogue)target).CurrentDialog = _selectedDialog;
            }
        }
        GUILayout.TextArea(_selectedDialog.Content);
        if (_selectedDialog.Exits != null)
        {
            for (int i = 0; i < _selectedDialog.Exits.Count; i++)
            {
                GUILayout.TextField(_selectedDialog.Exits[i].Target);
            }
        }
        base.OnInspectorGUI();
    }

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
            if(d.Target != null && d.Target.Length > 0)
            {
                e.Target = d.Target;
            } else if (line.Length > 1)
            {
                e.Target = line[1].Trim();
            } else
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
