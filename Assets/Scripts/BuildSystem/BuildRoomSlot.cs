using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomSlot : MonoBehaviour
{

    public SlotID ID;
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
        Instantiate(TargetObject, new Vector3(0, 8, 0), Quaternion.identity, transform);
        TargetObject.type = roomType;
    }
}

