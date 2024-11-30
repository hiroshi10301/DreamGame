using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum WorldType
{
    Magic,
    ModernCity,
    FutureCity,
}
public enum TentacleType
{
    MagicWildCreature,
    MagicExperimentCreature,
    ScienceExperimentCreature,
    NormalCreature,
}
public enum RoomType
{
    Empty,
    ElecticPower,
    MagicPower,
    Food,
    Slave,
}
[System.Serializable]
public class SlotID
{
    public int Layer;
    public int Slot;
}
public class GameLogicController :MonoBehaviour
{
    public Room EmptyRoom;
    public BuildRoomMenuController BuildRoomMenuController;
    public List<LayerController> LayerControllers;
    public SlotID CurrentSlotID;
    public WorldType worldType;
    public TentacleType tentacleType;
    public static GameLogicController Instance { get; private set; }
    private void OnEnable()
    {
        MessageManager.AddListener(MessageType.ClickBuildSlot, OpenBuildRoomMenu);
        MessageManager.AddListener(MessageType.BuildRoom, BuildRoom);
    }
    private void OnDisable()
    {
        MessageManager.RemoveListener(MessageType.ClickBuildSlot, OpenBuildRoomMenu);
        MessageManager.RemoveListener(MessageType.BuildRoom, BuildRoom);
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenBuildRoomMenu(IMessage message)
    {
        var CurrentID = message.Data as SlotID;
        BuildRoomMenuController.gameObject.SetActive(true);
        CurrentSlotID = CurrentID;
        BuildRoomMenuController.SpawnOption();
    }
    public void BuildRoom(IMessage message)
    {
        var CurrentRoomType = (RoomType)message.Data;
        var TargetLayer =  LayerControllers.FirstOrDefault(x => x.LayerID == CurrentSlotID.Layer);
        TargetLayer.SpawnRoom(CurrentRoomType);
    }
}
