using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildOption : MonoBehaviour
{

    public RoomType roomType;
    public void BuildRoom()
    {
        MessageManager.SendMessage(MessageType.BuildRoom, roomType);
    }
}
