using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUpdate
{
    public int tick;
    public Vector3 position;

    public GameObjectUpdate(int _tick, Vector3 _position)
    {
        tick = _tick;
        position = _position;
    }
}
