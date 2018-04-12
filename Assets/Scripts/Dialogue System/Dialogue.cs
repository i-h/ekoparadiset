using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
    public static int Score;
    public Transform DialogPanel;
    public Text DialogTextBox;
    public Text[] OptionText = new Text[3];
    bool _displayed = false;
    public DialogStruct CurrentDialog;
    public DialogStruct NextDialog;
    public string Content = "";
    public DialogExit[] Exits;
    public string Target = "";
    public DialogAsset[] Assets;
    public int NextIndex = 0;
    public int CurrentIndex = 0;
    int _correctIndex = -1;

    private void OnTriggerStay2D(Collider2D c)
    {
        if (!_displayed && c.tag.Contains("Player"))
        {
            Debug.Log("Launching dialogue");
            StartCoroutine(ShowDialogue());
        }
    }

    IEnumerator ShowDialogue()
    {
        Movable.MovementEnabled = false;
        _displayed = true;
        DialogPanel.gameObject.SetActive(true);
        while (NextIndex >= 0 && NextIndex < Assets.Length)
        {
            DialogTextBox.text = "";
            for (int i = 0; i < OptionText.Length; i++)
            {
                OptionText[i].text = "";
            }
            CurrentIndex = NextIndex;
            NextIndex = Assets[CurrentIndex].TargetIndex;
            DialogStruct current = DialogueTreeEditor.ParseDialogue(Assets[CurrentIndex].Content);
            for (int i = 0; i < current.Content.Length; i++)
            {
                float delay = 0.06f;
                DialogTextBox.text += current.Content[i];
                if (current.Content[i] == '.') delay = 0.5f;
                if (GamepadControls.State.Buttons.A == XInputDotNetPure.ButtonState.Pressed) delay = 0.0f;
                yield return new WaitForSeconds(delay);
            }
            //Debug.Log(current.Content);
            for(int i = 0; i < current.Exits.Count; i++)
            {
                OptionText[i].text = current.Exits[i].Prompt;
                if (current.Exits[i].Points > 0)
                {
                    _correctIndex = i;
                }
            }
            while (GamepadControls.State.Buttons.A != XInputDotNetPure.ButtonState.Released)
                yield return null;

            if (current.Exits.Count == 0)
            {
                while (GamepadControls.State.Buttons.A != XInputDotNetPure.ButtonState.Pressed)
                    yield return null;
            } else
            {
                bool up = false;
                bool right = false;
                bool down = false;
                do
                {
                    up = GamepadControls.State.DPad.Up == XInputDotNetPure.ButtonState.Pressed;
                    right = GamepadControls.State.DPad.Right == XInputDotNetPure.ButtonState.Pressed;
                    down = GamepadControls.State.DPad.Down == XInputDotNetPure.ButtonState.Pressed;
                    yield return null;
                } while (!up && !right && !down);
                switch (_correctIndex)
                {
                    case 0:
                        if (up)
                            Score++;
                        break;
                    case 1:
                        if (right)
                            Score++;
                        break;
                    case 2:
                        if (down)
                            Score++;
                        break;
                }
                Debug.Log(Score);
            }
        }

        DialogPanel.gameObject.SetActive(false);
        Movable.MovementEnabled = true;
        yield return null;
    }
}
