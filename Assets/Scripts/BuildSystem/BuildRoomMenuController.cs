using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomMenuController : MonoBehaviour
{
    public BuildOption OptionPrefab;
    public GameObject Content;
    private void OnEnable()
    {
        //SpawnOption();
    }
    private void OnDisable()
    {

        DestroyOption();
    }
    public void SpawnOption()
    {
        //baseic room

        //
        var WorldType = GameLogicController.Instance.worldType;
        var TentacleType = GameLogicController.Instance.tentacleType;
        switch (WorldType)
        {
            case WorldType.FutureCity:
                switch (TentacleType)
                {
                    case TentacleType.MagicExperimentCreature:
                        break;
                    case TentacleType.MagicWildCreature: 
                        break;
                    case TentacleType.NormalCreature:
                        break;
                    case TentacleType.ScienceExperimentCreature:
                        break;
                }
                break;
            case WorldType.Magic:
                switch (TentacleType)
                {
                    case TentacleType.MagicExperimentCreature:
                        break;
                    case TentacleType.MagicWildCreature:
                        break;
                    case TentacleType.NormalCreature:
                        break;
                    case TentacleType.ScienceExperimentCreature:
                        break;
                }
                break;
            case WorldType.ModernCity:
                switch (TentacleType)
                {
                    case TentacleType.MagicExperimentCreature:
                        break;
                    case TentacleType.MagicWildCreature:
                        break;
                    case TentacleType.NormalCreature:
                        break;
                    case TentacleType.ScienceExperimentCreature:
                        break;
                }
                break;

        }
        
    }

    public void DestroyOption()
    {

    }
}
