using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickObject : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 滑鼠左鍵點擊
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 獲取被點擊的物件
                GameObject clickedObject = hit.collider.gameObject;
                MessageManager.SendMessage(MessageType.ClickObject, clickedObject);
            }
        }
        if (Input.touchCount > 0)
        {
            // 獲取第一個觸摸點
            Touch touch = Input.GetTouch(0);

            // 判斷觸摸類型為觸摸開始
            if (touch.phase == TouchPhase.Began)
            {
                // 從觸摸點生成一條射線
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // 獲取被觸碰的物件
                    GameObject touchedObject = hit.collider.gameObject;

                    // 嘗試調用父物件上的 ClickableObject 腳本方法
                    MessageManager.SendMessage(MessageType.ClickObject, touchedObject);
                }
            }
        }
    }
}
