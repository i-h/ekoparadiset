using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordPair
{
    public string se;
    public string fi;
    public override string ToString()
    {
        return se + " - " + fi;
    }
}
