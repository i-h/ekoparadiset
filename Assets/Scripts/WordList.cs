using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordList {
    public static List<WordPair> Words = new List<WordPair>();
    public static void Load()
    {
        Words = new List<WordPair>();
        StreamReader sr = new StreamReader("Assets/vocabulary.txt");
        while (!sr.EndOfStream)
        {
            WordPair wordLine = new WordPair();
            string[] line = sr.ReadLine().Split('-');
            wordLine.fi = line[0].Trim();
            wordLine.se = line[1].Trim();
            Words.Add(wordLine);
        }
        Debug.Log("Word count: " + Words.Count);
    }

    public static string[] GetArray()
    {
        string[] wArray = new string[Words.Count];
        for (int i = 0; i < Words.Count; i++)
        {
            wArray[i] = Words[i].ToString();
        }
        return wArray;
    }

    public static WordPair RandomWord()
    {
        Load();
        int randIndex = Random.Range(0, Words.Count);
        return Words[randIndex];
    }
}
