using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Experimental.GraphView.GraphView;
public class LayerController : MonoBehaviour
{
    public int LayerID;
    public List<BuildRoomSlot> BuildRoomSlots;
    public int minObjectsPerSide;
    public int maxObjectsPerSide;
    public GameObject SlotPrefab;
    public GameObject Context;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void Start()
    {
        SpawnBuildingSlot();
    }
    public void SpawnBuildingSlot()
    {
        SpawnObjectsOnCorridor();
    }

    private void SpawnObjectsOnCorridor()
    {
        // 獲取 Plane 的尺寸
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("This script must be attached to a Plane with a MeshRenderer!");
            return;
        }
        
        Vector3 planeSize = meshRenderer.bounds.size;
        float planeLength = planeSize.z; // 走廊的長度
        float planeWidth = planeSize.x;  // 走廊的寬度

        // 計算起始位置（兩側的 x 軸）
        float leftSideX = -planeWidth / 2; // 左側的 x 坐標
        float rightSideX = planeWidth / 2; // 右側的 x 坐標

        // 隨機生成數量
        int leftSideObjects = Random.Range(minObjectsPerSide, maxObjectsPerSide + 1);
        int rightSideObjects = Random.Range(minObjectsPerSide, maxObjectsPerSide + 1);
        int currentSlotID = 1;
        currentSlotID = GenerateObjectsOnSide(leftSideX, planeLength, leftSideObjects, "Left", currentSlotID);

        // 生成右側物件
        GenerateObjectsOnSide(rightSideX, planeLength, rightSideObjects, "Right", currentSlotID);
    }

    private int GenerateObjectsOnSide(float sideX, float length, int objectCount, string sideName, int startingSlotID)
    {
        // 計算物件之間的間距
        float spacing = length / (objectCount + 1);
        int currentSlotID = startingSlotID;

        for (int i = 0; i < objectCount; i++)
        {
            // 計算每個物件的 z 軸位置
            float zPosition = -length / 2 + spacing * (i + 1);

            // 設定生成位置
            Vector3 spawnPosition = new Vector3(sideX, 0, zPosition);

            // 生成物件
            GameObject newObject = Instantiate(SlotPrefab,Context.transform.position + spawnPosition, Quaternion.identity,Context.transform);
            newObject.name = $"{sideName}_Object_{currentSlotID}";

            // 分配 SlotID
            SlotID slotID = new SlotID
            {
                Layer = LayerID, // 從外部設置的 Layer ID
                Slot = currentSlotID // 根據生成順序分配 Slot 編號
            };

            // 設置到生成的物件上
            newObject.GetComponent<BuildRoomSlot>().ID = slotID;

            // 自增 Slot 編號
            currentSlotID++;
        }

        return currentSlotID;
    }

    public void SpawnRoom(RoomType roomType)
    {
        var TargetSlot = BuildRoomSlots.FirstOrDefault(x=>x.ID.Slot == GameLogicController.Instance.CurrentSlotID.Slot);
        TargetSlot.BuildRoomHere(roomType);
    }
}
