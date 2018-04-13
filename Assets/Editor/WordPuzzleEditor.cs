using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
[CustomEditor(typeof(WordPuzzle))]
public class WordPuzzleEditor : Editor {
    public WordPair[] Vocabulary;
    WordPuzzle _tgt;
    int _selectedWordIndex = 0;
    WordPair _selectedWord;
    bool _random = false;
    public override void OnInspectorGUI()
    {
        if(WordList.Words.Count == 0)
        {
            WordList.Load();
        }
        _tgt = (WordPuzzle)target;
        if (GUILayout.Button("Refresh wordlist"))
        {
            WordList.Load();
        }
        if (_selectedWord != null)
        {
            _selectedWord = _tgt.Word;
        }
        GUILayout.BeginHorizontal();
        _selectedWordIndex = EditorGUILayout.Popup(_selectedWordIndex, WordList.GetArray());
        if (GUILayout.Button("Set"))
        {
            _selectedWord = WordList.Words[_selectedWordIndex];
            ((WordPuzzle)target).Word = _selectedWord;
        }
        ((WordPuzzle)target).RandomWord = GUILayout.Toggle(_tgt.RandomWord, "Random");
        GUILayout.EndHorizontal();
        if (_tgt.Word != null)
        {            
            GUILayout.Label("Selected word: " + _tgt.Word.ToString());
        }
        base.OnInspectorGUI();
    }    
}
