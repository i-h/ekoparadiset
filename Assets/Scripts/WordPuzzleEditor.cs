using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
[CustomEditor(typeof(WordPuzzle))]
public class WordPuzzleEditor : Editor {
    public WordPair[] Vocabulary;
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Refresh wordlist"))
        {
            WordList.Load();
        }
        base.OnInspectorGUI();
    }    
}

[System.Serializable]
public struct WordPair
{
    public string se;
    public string fi;
}
