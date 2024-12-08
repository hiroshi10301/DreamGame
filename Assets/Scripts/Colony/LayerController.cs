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
    public GameObject ContextPrefab;
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
        var ConTextPos = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
        Context = Instantiate(ContextPrefab, ConTextPos,Quaternion.identity);
        Context.name = "Layer" + LayerID + "Context";
        //生成Context，並將之後的slot生成在Context之下，避免被layer的scale變化給拉伸變形

        BoxCollider boxCollider = SlotPrefab.GetComponent<BoxCollider>();
        // 動態獲取物件寬度
        float objectWidth = boxCollider.size.x * SlotPrefab.transform.localScale.x;
        // 檢查plane是否有meshRender、獲取 Plane 的尺寸
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
        float leftSideX = -planeWidth / 2 + objectWidth / 2; // 左側的 x 坐標，在寬度一半的、x在plane中央左邊位置，然後再往右移動prefab一半寬度，這樣prefab就會在走廊內貼期的剛剛好
        float rightSideX = planeWidth / 2 - objectWidth / 2; // 右側的 x 坐標

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
            GameObject newSlotObject = Instantiate(SlotPrefab,Context.transform.position + spawnPosition, Quaternion.identity,Context.transform);
            newSlotObject.name = $"{sideName}_Object_{currentSlotID}";

            // 分配 SlotID
            SlotID slotID = new SlotID
            {
                Layer = LayerID, // 從外部設置的 Layer ID
                Slot = currentSlotID // 根據生成順序分配 Slot 編號
            };

            // 設置到生成的物件上
            var SlotComponent = newSlotObject.GetComponent<BuildRoomSlot>();
            SlotComponent.ID = slotID;
            BuildRoomSlots.Add(SlotComponent);

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
