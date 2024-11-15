using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoom : MonoBehaviour
{
    public GameObject RoomPrefab;

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
            MessageManager.SendMessage(MessageType.OpenBuildRoomUI, this);
        }
            
    }
}
