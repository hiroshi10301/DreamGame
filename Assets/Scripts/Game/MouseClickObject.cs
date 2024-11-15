using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickObject : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �ƹ������I��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // ����Q�I��������
                GameObject clickedObject = hit.collider.gameObject;
                MessageManager.SendMessage(MessageType.ClickObject, clickedObject);
            }
        }
        if (Input.touchCount > 0)
        {
            // ����Ĥ@��Ĳ�N�I
            Touch touch = Input.GetTouch(0);

            // �P�_Ĳ�N������Ĳ�N�}�l
            if (touch.phase == TouchPhase.Began)
            {
                // �qĲ�N�I�ͦ��@���g�u
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // ����QĲ�I������
                    GameObject touchedObject = hit.collider.gameObject;

                    // ���սեΤ�����W�� ClickableObject �}����k
                    MessageManager.SendMessage(MessageType.ClickObject, touchedObject);
                }
            }
        }
    }
}
