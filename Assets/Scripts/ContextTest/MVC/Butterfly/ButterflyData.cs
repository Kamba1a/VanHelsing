﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewButterfly", menuName = "CreateData/Butterfly", order = 0)]
public class ButterflyData : ScriptableObject
{
    #region Fields

    private readonly float sin60 = Mathf.Sqrt(3) / 2;
    public ButterflyStruct Struct;

    #endregion


    #region Metods

    /// <summary>Return random coordinates within MaxDistanceFromCurrentPosition</summary>
    public Vector3 NewTargetPoint(Vector3 currentPosition)
    {
        float x = GetRandomCoord(currentPosition.x);
        float y = GetRandomCoord(currentPosition.y);
        float z = GetRandomCoord(currentPosition.z);
        return new Vector3(x, y, z);
    }

    /// <summary>Return random coordinates within MaxDistanceFromCurrentPosition in the opposite direction along the specified coordinate axis</summary>
    public Vector3 NewTargetPointInOppositeDirection(Vector3 currentPosition, Vector3 currentDirection, string axis)
    {
        Vector3 newTarget = NewTargetPoint(currentPosition);

        switch (axis.ToUpper())
        {
            case "X": newTarget.x = GetRandomCoord(currentPosition.x, currentDirection.x); break;
            case "Y": newTarget.y = GetRandomCoord(currentPosition.y, currentDirection.y); break;
            case "Z": newTarget.z = GetRandomCoord(currentPosition.z, currentDirection.z); break;
        }

        return newTarget;
    }

    private float GetRandomCoord(float coord, float? forwardCoord = null)
    {
        if (forwardCoord.HasValue && forwardCoord.Value != 0)
        {
            if (forwardCoord > 0) return Random.Range(coord - Struct.MaxDistanceFromCurrentPosition, coord);
            else return Random.Range(coord, coord + Struct.MaxDistanceFromCurrentPosition);
        }
        return Random.Range(coord - Struct.MaxDistanceFromCurrentPosition, coord + Struct.MaxDistanceFromCurrentPosition); ;
    }

    public Vector3 Move(Vector3 currentPosition, Vector3 targetPoint)
    {
        return Vector3.MoveTowards(currentPosition, targetPoint, Struct.Speed);
    }

    public Quaternion Turn(Transform transform, Vector3 targetPoint)
    {
        Vector3 targetDirection = targetPoint - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Struct.TurnSpeed, 0.0f);
        return Quaternion.LookRotation(new Vector3(newDirection.x, -sin60, newDirection.z));
    }

    #endregion
}
