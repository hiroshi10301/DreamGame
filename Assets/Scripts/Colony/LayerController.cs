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
        //�ͦ�Context�A�ñN���᪺slot�ͦ��bContext���U�A�קK�Qlayer��scale�ܤƵ��Ԧ��ܧ�

        BoxCollider boxCollider = SlotPrefab.GetComponent<BoxCollider>();
        // �ʺA�������e��
        float objectWidth = boxCollider.size.x * SlotPrefab.transform.localScale.x;
        // �ˬdplane�O�_��meshRender�B��� Plane ���ؤo
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("This script must be attached to a Plane with a MeshRenderer!");
            return;
        }

        Vector3 planeSize = meshRenderer.bounds.size;
        float planeLength = planeSize.z; // ���Y������
        float planeWidth = planeSize.x;  // ���Y���e��

        // �p��_�l��m�]�ⰼ�� x �b�^
        float leftSideX = -planeWidth / 2 + objectWidth / 2; // ������ x ���СA�b�e�פ@�b���Bx�bplane���������m�A�M��A���k����prefab�@�b�e�סA�o��prefab�N�|�b���Y���K�������n
        float rightSideX = planeWidth / 2 - objectWidth / 2; // �k���� x ����

        // �H���ͦ��ƶq
        int leftSideObjects = Random.Range(minObjectsPerSide, maxObjectsPerSide + 1);
        int rightSideObjects = Random.Range(minObjectsPerSide, maxObjectsPerSide + 1);
        int currentSlotID = 1;
        currentSlotID = GenerateObjectsOnSide(leftSideX, planeLength, leftSideObjects, "Left", currentSlotID);

        // �ͦ��k������
        GenerateObjectsOnSide(rightSideX, planeLength, rightSideObjects, "Right", currentSlotID);
    }
    private int GenerateObjectsOnSide(float sideX, float length, int objectCount, string sideName, int startingSlotID)
    {
        // �p�⪫�󤧶������Z
        float spacing = length / (objectCount + 1);
        int currentSlotID = startingSlotID;

        for (int i = 0; i < objectCount; i++)
        {
            // �p��C�Ӫ��� z �b��m
            float zPosition = -length / 2 + spacing * (i + 1);

            // �]�w�ͦ���m
            Vector3 spawnPosition = new Vector3(sideX, 0, zPosition);

            // �ͦ�����
            GameObject newSlotObject = Instantiate(SlotPrefab,Context.transform.position + spawnPosition, Quaternion.identity,Context.transform);
            newSlotObject.name = $"{sideName}_Object_{currentSlotID}";

            // ���t SlotID
            SlotID slotID = new SlotID
            {
                Layer = LayerID, // �q�~���]�m�� Layer ID
                Slot = currentSlotID // �ھڥͦ����Ǥ��t Slot �s��
            };

            // �]�m��ͦ�������W
            var SlotComponent = newSlotObject.GetComponent<BuildRoomSlot>();
            SlotComponent.ID = slotID;
            BuildRoomSlots.Add(SlotComponent);

            // �ۼW Slot �s��
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
