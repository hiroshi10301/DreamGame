using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOption : MonoBehaviour
{
    public enum RoomType
    {
        Power,
        Food,
        Slave,
    }
    public RoomType roomType;
}
