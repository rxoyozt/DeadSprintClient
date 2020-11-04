using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public bool[] inputs;

    private List<GameObjectUpdate> objectUpdates = new List<GameObjectUpdate>();
    private GameObjectUpdate interpolatePlayerTo;
    private GameObjectUpdate interpolatePlayerFrom;
    private GameObjectUpdate interpolatePlayerPrevious;

    private float timeElapsed;
    private float timeToReach;

    private float moveSpeed = 5f / Constants.TICKS_PER_SEC;

    public void Start()
    {
        interpolatePlayerTo = new GameObjectUpdate(GameManager.instance.tick, interpolatePlayerTo.position);
        interpolatePlayerPrevious = new GameObjectUpdate(GameManager.instance.tickDelay, interpolatePlayerTo.position);
        interpolatePlayerFrom = new GameObjectUpdate(GameManager.instance.tickDelay, interpolatePlayerTo.position);
    }

    /* now working on input prediction
    public void FixedUpdate()
    {
        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (inputs[3])
        {
            _inputDirection.x += 1;
        }

        move(_inputDirection);
    }
    */

    private void Update()
    {

        //entity interpolation
        for (int i = 0; i < objectUpdates.Count; i++)
        {
            if (GameManager.instance.tick >= objectUpdates[i].tick)
            {
                //note: implement previous interpolation object to use it to prevent extrapolating
                interpolatePlayerFrom = interpolatePlayerTo;
                interpolatePlayerTo = objectUpdates[i];
                objectUpdates.RemoveAt(i);
                timeElapsed = 0;
                timeToReach = (interpolatePlayerTo.tick - interpolatePlayerFrom.tick) * Constants.TICKS_PER_SEC;
            }
        }

        timeElapsed += 1;
        interpolatePosition(timeElapsed / timeToReach);
    }
    private void move(Vector2 _inputDirection)
    {
        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.up * _inputDirection.y;
        this.transform.position += _moveDirection * moveSpeed;
    }

    private void inputComparePrediction(int _tick, Vector3 _position)
    {
        //compare the predicted input
        //if statement to correct position to actual position
    }

    private void syncToActualPosition(Vector3 _position)
    {
        //correct the position
    }

    #region player interpolation
    public void interpolatePosition(float _lerp)
    {
        this.transform.position = Vector3.Lerp(interpolatePlayerFrom.position, interpolatePlayerTo.position, _lerp);
    }

    public void newObjectUpdate(int _tick, Vector3 _position)
    {
        Debug.Log($"{_position}");
        if (_tick <= GameManager.instance.tickDelay)
        {
            return;
        }

        if (objectUpdates.Count == 0)
        {
            objectUpdates.Add(new GameObjectUpdate(_tick, _position));
            return;
        }

        for (int i = 0; i < objectUpdates.Count; i++)
        {
            if (_tick < objectUpdates[i].tick)
            {
                objectUpdates.Insert(i, new GameObjectUpdate(_tick, _position));
                break;
            }
        }
    }
    #endregion
}
