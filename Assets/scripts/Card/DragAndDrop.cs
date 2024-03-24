using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera; // 引用主相机
    private Vector3 screenPoint; // 鼠标在屏幕上的初始位置
    private Vector3 offset; // 物体在世界空间中的初始偏移量
    private bool isDragging = false; // 是否正在拖拽

    void Start()
    {
        mainCamera = Camera.main; // 获取主相机
    }

    void Update()
    {
        if (isDragging)
        {
            // 计算鼠标在屏幕上的位置
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            // 将屏幕坐标转换为世界坐标
            Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
            cursorPosition.z = -1;
            // 更新物体的位置
            transform.position = cursorPosition;
        }
    }

    void OnMouseDown()
    {
        // 当鼠标按下时，记录屏幕坐标和物体的偏移量
        screenPoint = mainCamera.WorldToScreenPoint(transform.position);
        offset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        // 开始拖拽
        isDragging = true;
    }

    void OnMouseUp()
    {
        // 当鼠标释放时，停止拖拽
        isDragging = false;
    }
}