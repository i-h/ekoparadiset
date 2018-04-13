using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : Movable {
    // Update is called once per frame
    void Update () {
        _dirVector.x = Input.GetAxisRaw("Horizontal");
        _dirVector.y = Input.GetAxisRaw("Vertical");

        Move(_dirVector.normalized);

    }
}
