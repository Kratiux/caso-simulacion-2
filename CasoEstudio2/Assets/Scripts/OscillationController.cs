using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OscillationController : MonoBehaviour
{
    [SerializeField]
    bool backAndForth;

    [SerializeField]
    bool reverse;

    [SerializeField]
    bool MoveInX;

    [SerializeField]
    bool MoveInY;

    [SerializeField]
    bool MoveInZ;

    [SerializeField]
    float travelDistance;

    [SerializeField]
    float speed;

    Vector3 _startPosition;
    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (!backAndForth)
        {
            return;
        }
        Vector3 position = transform.position;
        if (!reverse)
        {
           if (MoveInX)
            {
                position.x = _startPosition.x + Mathf.PingPong(Time.time * speed, travelDistance) * -Helper.BoolToInt(reverse);
                transform.position = position;
            }
            if (MoveInY)
            {
                position.y = _startPosition.y + Mathf.PingPong(Time.time * speed, travelDistance) * -Helper.BoolToInt(reverse);
                transform.position = position;
            }
            if (MoveInZ)
            {
                position.z = _startPosition.z + Mathf.PingPong(Time.time * speed, travelDistance) * -Helper.BoolToInt(reverse);
                transform.position = position;
            }
        }
    }
}
