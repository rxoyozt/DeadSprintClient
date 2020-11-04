using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void FixedUpdate()
    {
        sendInputToServer();
    }

    private void sendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D)
        };

        ClientSend.playerMovement(_inputs);
        GameManager.players[Client.instance.myId].inputs = _inputs;
    }
}
