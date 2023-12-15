using System;
using UnityEngine;

[Serializable]
public struct MouseSensitivity
{
    [SerializeField]
    public float horizontal;

    [SerializeField]
    public float vertical;

    [SerializeField]
    public bool invertHorizontal;

    [SerializeField]
    public bool invertVertical;
}
