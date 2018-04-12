using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WordPuzzle))]
public class WordItem : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player1")
        {
            StartPuzzle();
        }
    }

    void StartPuzzle()
    {
        WordPuzzle p = GetComponent<WordPuzzle>();
        if (p != null)
        {
            p.Begin(transform);
        }
    }
}
