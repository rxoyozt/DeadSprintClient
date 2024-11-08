﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void sendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.sendData(_packet);
    }

    private static void sendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.sendData(_packet);
    }

    public static void welcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            sendTCPData(_packet);
        }
    }

    public static void playerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            sendUDPData(_packet);
        }
    }
}
