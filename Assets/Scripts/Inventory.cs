using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    static InventoryPanel _panel;
    static float _rowHeight = 32;
    static List<WordPair> _inv = new List<WordPair>();
    public static void GainItem(WordPair itm)
    {
        _inv.Add(itm);
        UpdatePanel();
    }    

    public static void UpdatePanel()
    {
        _panel = GameObject.FindWithTag("InventoryPanel").GetComponent<InventoryPanel>();

        _panel.WordListPanel.text = "";
        for (int i = 0; i < _inv.Count; i++)
        {
            _panel.WordListPanel.text += _inv[i].ToString() + "\n";
        }
    }
}
