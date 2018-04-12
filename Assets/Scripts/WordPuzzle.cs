using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordPuzzle : MonoBehaviour {
    public static int[] HintIntervals = { 8, 12, 15 };
    public WordPair Word = new WordPair();
    public WordPuzzleUI WordPuzzleUIprefab;
    WordPuzzleUI PuzzleUIobj;
    bool _active = false;
    bool _completed = false;
    float _startTime = -1;
    float _elapsed = 0;
    int _hintPhase = 0;
    int _hintLetters = 0;
    Transform _source;

	
	// Update is called once per frame
	void Update () {
        if (_active)
        {
            _elapsed = Time.time - _startTime;
            PuzzleUIobj.WordInput.text += Input.inputString;
            if(PuzzleUIobj.WordInput.text.ToUpper() == Word.se.ToUpper())
            {
                Completed(true);
            } else if(PuzzleUIobj.WordInput.text.Length >= Word.se.Length)
            {
                Completed(false);
            }
            GiveHints(_elapsed);
        }
	}

    void GiveHints(float time)
    {
        // Hint intervals, 4, 8, 12, 15 ...
        if (_hintLetters == Word.se.Length)
        {
            Completed(false);
        } else if (time > HintIntervals[2] + _hintLetters && _hintLetters < Word.se.Length)
        {
            _hintLetters++;
            PuzzleUIobj.WordHint.text = "";
            for (int i = 0; i < _hintLetters; i++) PuzzleUIobj.WordHint.text += Word.se[i] + " ";
            for (int i = _hintLetters; i < Word.se.Length; i++) PuzzleUIobj.WordHint.text += "_ ";


            Debug.Log("Phase 3 activated");

        } else if (time > HintIntervals[1] && _hintPhase < 2)
        {
            _hintPhase = 2;
            PuzzleUIobj.WordHint.text = Word.se[0] + " ";
            for (int i = 1; i < Word.se.Length; i++) PuzzleUIobj.WordHint.text += "_ ";
            Debug.Log("Phase 2 activated");

        } else if (time > HintIntervals[0] && _hintPhase < 1)
        {
            _hintPhase = 1;
            PuzzleUIobj.WordHint.text = "";
            for (int i = 0; i < Word.se.Length; i++) PuzzleUIobj.WordHint.text += "_ ";
            Debug.Log("Phase 1 activated");
        }
    }    

    public void Begin(Transform source)
    {
        _source = source;
        if (WordPuzzleUIprefab == null)
        {
            Debug.LogError(string.Format("Puzzle UI for {0} is null!", Word));
            return;
        }
        if (_completed) return;
        GameObject c = GameObject.FindWithTag("Canvas");
        PuzzleUIobj = Instantiate<WordPuzzleUI>(WordPuzzleUIprefab, c.transform);
        PuzzleUIobj.transform.name = "Puzzle: " + Word;
        PuzzleUIobj.WordInput.text = "";
        PuzzleUIobj.WordHint.text = "___";
        PuzzleUIobj.WordPrompt.text = Word.fi;

        _active = true;
        Movable.MovementEnabled = false;
        _startTime = Time.time;
        _hintPhase = 0;
        _hintLetters = 0;
    }

    public void Completed(bool success)
    {
        _active = false;
        Movable.MovementEnabled = true;
        _completed = success;
        Debug.Log("Mission ended, success: " + success);
        if (success)
        {
            Inventory.GainItem(Word);
            Destroy(_source.gameObject);
        }

        Destroy(PuzzleUIobj.gameObject);
    }
}
