using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    //ClientHadle class will process the packet that has been received to be read and use the data sent from the server.
    public static void welcome(Packet _packet)
    {
        //note: write the code in order the same way from ServerSend class at least for TCP packets

        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from the server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.welcomeReceived();

        Client.instance.udp.connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void spawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector2 _position = _packet.ReadVector2();

        GameManager.instance.spawnPlayer(_id, _username, _position);
    }

    public static void playerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        GameManager.players[_id].newObjectUpdate(GameManager.instance.tick, _position);
    }

    public static void playerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int _tick = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();
        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void removePlayerObject(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }
}
