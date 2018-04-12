using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WordPuzzle))]
public class HiddenWord : MonoBehaviour {
    [Range(0,1)]
    public float ThresholdDistance = 0.5f;
    public float MaxDistance = 2.0f;
    bool _hidden = true;

    private void OnTriggerStay2D(Collider2D c)
    {
        if (!_hidden)
        {
            GamepadControls.SetVibration(0, 0);
        }
        if (c.tag == "Player2")
        {
            Vector3 dir = c.transform.position - transform.position;
            float vStr = Mathf.Pow(1 - Mathf.Clamp(dir.magnitude / MaxDistance,0.0f,1.0f), 2);

            GamepadControls.SetVibration(0, vStr);

            if(vStr > ThresholdDistance)
            {
                if (GamepadControls.PrevState.Buttons.A == XInputDotNetPure.ButtonState.Pressed)
                {
                    RevealPuzzle();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player2")
        {
            GamepadControls.SetVibration(0, 0);
        }
    }
    
    void RevealPuzzle()
    {
        if (_hidden)
        {
            WordPuzzle p = GetComponent<WordPuzzle>();
            p.Begin(transform);
            _hidden = false;
        }
    }
}
