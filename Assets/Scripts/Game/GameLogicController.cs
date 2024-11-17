using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class GameLogicController :MonoBehaviour
{

    public WorldType worldType;
    public TentacleType tentacleType;
    public static GameLogicController Instance { get; private set; }
    private void OnEnable()
    {
        MessageManager.AddListener(MessageType.ClickBuildSlot, OpenBuildRoomMenu);
    }
    private void OnDisable()
    {
        MessageManager.RemoveListener(MessageType.ClickBuildSlot, OpenBuildRoomMenu);
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
    public BuildRoomMenuController BuildRoomMenuController;
    public void OpenBuildRoomMenu(IMessage message)
    {
        var CurrentID = message.Data as SlotID;
        BuildRoomMenuController.gameObject.SetActive(true);
        BuildRoomMenuController.CurrentSlotID = CurrentID;
    }
}
