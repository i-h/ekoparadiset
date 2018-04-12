using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    static List<WordPair> _inv = new List<WordPair>();
    public static void GainItem(WordPair itm)
    {
        _inv.Add(itm);
    }
}
