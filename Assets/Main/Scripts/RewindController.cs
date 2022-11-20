using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PointInTime
{
    public Vector3 Position;
    public Quaternion Rotation;

    public PointInTime(Vector3 _position, Quaternion _rotation)
    {
        Position = _position;
        Rotation = _rotation;
    }
}

public class RewindController : MonoBehaviour
{
    [SerializeField] private int maxTimePoints = 300;
    List<PointInTime> pointsInTime = new List<PointInTime>();
    bool isRewinding;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RewindTime();
    }

    private void FixedUpdate() //to record at constant interval
    {
        if (isRewinding)
            RewindTimePoints();
        else
            RecordTimePoints();
    }

    private void RecordTimePoints()
    {
        if(pointsInTime.Count >= maxTimePoints) //if greater than range remove first recorded point
            pointsInTime.RemoveAt(0); 

        PointInTime _timePoint = new PointInTime(transform.position, transform.rotation);
        pointsInTime.Add(_timePoint);
    }

    private void RewindTimePoints()
    {
        if(pointsInTime.Count >0)
        {
            SetPointInTime(pointsInTime[pointsInTime.Count - 1]);
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        else
        {
            StopRewinding();
        }
    }

    private void SetPointInTime(PointInTime _pointInTime)
    {
        transform.position = _pointInTime.Position;
        transform.rotation = _pointInTime.Rotation;
    }

    private void StopRewinding()
    {
        isRewinding = false;
    }

    public void RewindTime()
    {
        isRewinding = true;
    }
}
