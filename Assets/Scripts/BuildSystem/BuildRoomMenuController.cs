using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomMenuController : MonoBehaviour
{
    public GameObject OptionPrefab;
    public GameObject Content;
    public SlotID CurrentSlotID;
    private void OnEnable()
    {
        SpawnOptionPrefab();
    }
    private void OnDisable()
    {

        DestroyOption();
    }
    public void SpawnOptionPrefab()
    {
        //baseic room

        //
        var WorldType = GameLogicController.Instance.worldType;
        var TentacleType = GameLogicController.Instance.tentacleType;
        switch (WorldType)
        {
            case WorldType.FutureCity:
                break;

        }
    }

    public void DestroyOption()
    {

    }
}
