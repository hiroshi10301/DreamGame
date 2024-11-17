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
        if (ClickedObject == this)
        {
            MessageManager.SendMessage(MessageType.ClickBuildSlot, ID);
        }
            
    }
}
public class SlotID
{
    public int Layer;
    public int Slot;
}
