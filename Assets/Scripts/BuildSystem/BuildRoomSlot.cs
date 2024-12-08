using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SlotSide 
{
    left,
    right
}

public class BuildRoomSlot : MonoBehaviour
{
    public SlotSide Side;
    public SlotID ID;
    public Room Room;
    private void OnEnable()
    {
        MessageManager.AddListener(MessageType.ClickObject, OpenBuildUI);
    }
    private void OnDisable()
    {
        MessageManager.RemoveListener(MessageType.ClickObject, OpenBuildUI);
    }
    public void OpenBuildUI(IMessage message)
    {
        var ClickedObject = message.Data as GameObject;
        if (ClickedObject == this.gameObject)
        {
            MessageManager.SendMessage(MessageType.ClickBuildSlot, ID);
            Debug.Log("Click Slot");
        }
            
    }
    public void BuildRoomHere(RoomType roomType)
    {
        var TargetObject = GameLogicController.Instance.EmptyRoom;
        if(Room == null)
        {
            Room = Instantiate(TargetObject, transform);
            Room.type = roomType;
        }
        else
        {
            ChangeRoom(roomType);
        }

    }
    public void ChangeRoom(RoomType roomType)
    {
        Destroy(Room);
        Room = Instantiate(GameLogicController.Instance.EmptyRoom, transform);
        Room.type = roomType;
    }
}

