using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LayerController : MonoBehaviour
{
    public int LayerID;
    public List<BuildRoomSlot> BuildRoomSlots;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    public void SpawnBuildingSlot()
    {

    }
    public void SpawnRoom(RoomType roomType)
    {
        var TargetSlot = BuildRoomSlots.FirstOrDefault(x=>x.ID.Slot == GameLogicController.Instance.CurrentSlotID.Slot);
        TargetSlot.BuildRoomHere(roomType);
    }
}
