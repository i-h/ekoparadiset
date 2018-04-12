using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
    public Transform DialogPanel;
    public Text DialogTextBox;
    bool _displayed = false;
    public DialogStruct CurrentDialog;
    public DialogStruct NextDialog;
    public string Content = "";
    public DialogExit[] Exits;
    public string Target = "";
    public DialogAsset[] Assets;
    public int NextIndex = 0;
    public int CurrentIndex = 0;

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
        bool listenForPress = true;
        Movable.MovementEnabled = false;
        _displayed = true;
        DialogPanel.gameObject.SetActive(true);
        while (NextIndex >= 0 && NextIndex < Assets.Length)
        {
            DialogTextBox.text = "";
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
            foreach(DialogExit e in current.Exits)
            {
                //Debug.Log(e.Prompt);
            }
            bool prevAreleased = GamepadControls.State.Buttons.A == XInputDotNetPure.ButtonState.Released;
            while (GamepadControls.State.Buttons.A != XInputDotNetPure.ButtonState.Released)
                yield return null;
            while (GamepadControls.State.Buttons.A != XInputDotNetPure.ButtonState.Pressed)
                yield return null;
        }

        DialogPanel.gameObject.SetActive(false);
        Movable.MovementEnabled = true;
        yield return null;
    }
}
