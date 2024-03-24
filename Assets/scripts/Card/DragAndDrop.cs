using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera; // ���������
    private Vector3 screenPoint; // �������Ļ�ϵĳ�ʼλ��
    private Vector3 offset; // ����������ռ��еĳ�ʼƫ����
    private bool isDragging = false; // �Ƿ�������ק

    void Start()
    {
        mainCamera = Camera.main; // ��ȡ�����
    }

    void Update()
    {
        if (isDragging)
        {
            // �����������Ļ�ϵ�λ��
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            // ����Ļ����ת��Ϊ��������
            Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
            cursorPosition.z = -1;
            // ���������λ��
            transform.position = cursorPosition;
        }
    }

    void OnMouseDown()
    {
        // ����갴��ʱ����¼��Ļ����������ƫ����
        screenPoint = mainCamera.WorldToScreenPoint(transform.position);
        offset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        // ��ʼ��ק
        isDragging = true;
    }

    void OnMouseUp()
    {
        // ������ͷ�ʱ��ֹͣ��ק
        isDragging = false;
    }
}