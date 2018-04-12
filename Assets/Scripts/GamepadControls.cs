using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamepadControls : Movable
{
    public static GamePadState State;
    public static GamePadState PrevState;
    static PlayerIndex _index;
    bool _playerIndexSet = false;

    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!_playerIndexSet || !PrevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex plrIndex = (PlayerIndex)i;
                GamePadState plrState = GamePad.GetState(plrIndex);
                if (plrState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", plrIndex));
                    _index = plrIndex;
                    _playerIndexSet = true;
                }
            }
        }

        PrevState = State;
        State = GamePad.GetState(_index);

        _dirVector.x = State.ThumbSticks.Left.X;
        _dirVector.y = State.ThumbSticks.Left.Y;
        Move(_dirVector);
    }

    public static void SetVibration(float lStr, float rStr)
    {
        GamePad.SetVibration(_index, lStr, rStr);
    }
}
