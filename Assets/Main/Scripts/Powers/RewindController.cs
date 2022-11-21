using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
    [SerializeField] private int maxTimePoints = 100;
    [SerializeField] private int rewindCount;
    [SerializeField] private TMP_Text text_count;
    [SerializeField] private PostProcessVolume volume;


    List<PointInTime> pointsInTime = new List<PointInTime>();
    bool isRewinding;
    public bool IsRewinding { get { return isRewinding; } }

    private int countLeft;
    public int CountLeft { get { return countLeft; } }


    private void Start()
    {
        countLeft = rewindCount;
        text_count.text = "rewind left : " + countLeft.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && countLeft > 0)
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
        gameObject.layer = LayerMask.NameToLayer("Default");
        if (volume.profile.TryGetSettings<ChromaticAberration>(out ChromaticAberration _ca))
        {
            _ca.intensity.value = 0f;
        }
    }

    public void RewindTime()
    {
        isRewinding = true;
        countLeft--;
        text_count.text = "rewind left : " + countLeft.ToString();
        gameObject.layer = LayerMask.NameToLayer("Rewind");
        if (volume.profile.TryGetSettings<ChromaticAberration>(out ChromaticAberration _ca))
        {
            _ca.intensity.value = 1f;
        }
    }
}
