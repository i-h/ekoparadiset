using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movable : MonoBehaviour
{
    public static bool MovementEnabled = true;
    public float MoveSensitivity = 2.0f;
    protected Vector2 _dirVector = new Vector2();

    Animator _anim;
    Rigidbody2D _rb;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 dir)
    {
        if (!MovementEnabled) return;
        dir = dir * Time.deltaTime * MoveSensitivity;
        _anim.SetFloat("MoveSpeed", dir.magnitude * Mathf.Sign(dir.x));
        _rb.MovePosition((Vector2)transform.position + dir);
        //transform.Translate(dir.x, dir.y, 0);
    }
}
