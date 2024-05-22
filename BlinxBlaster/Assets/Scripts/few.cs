using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class few : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float objectSize;
    public int numObjects;

    void Start()
    {
        // ���������� ������ �����, ����������� �� ������� ������� � ���������� ��������
        float radius = objectSize * numObjects / (2 * Mathf.PI);

        // ������� ������ ��� �������� ���������� ��������
        GameObject[] spawnedObjects = new GameObject[numObjects];

        // ������� �� �����
        for (int i = 0; i < numObjects; i++)
        {
            // ���������� ���� �������� ��� ������� �������
            float angle = 2 * Mathf.PI * i / numObjects;

            // ���������� ������� ������� �� ����������
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

            // ������� ������
            GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);

            // �������� ������ � ������
            spawnedObjects[i] = spawnedObject;
        }
    }
}
