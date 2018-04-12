using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public static bool MovementEnabled = true;
    public float MoveSensitivity = 2.0f;
    protected Vector2 _dirVector = new Vector2();

    public void Move(Vector2 dir)
    {
        if (!MovementEnabled) return;
        dir = dir * Time.deltaTime * MoveSensitivity;
        transform.Translate(dir.x, dir.y, 0);
    }
}
