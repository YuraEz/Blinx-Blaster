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
        // Рассчитать радиус круга, основываясь на размере объекта и количестве объектов
        float radius = objectSize * numObjects / (2 * Mathf.PI);

        // Создать массив для хранения спавненных объектов
        GameObject[] spawnedObjects = new GameObject[numObjects];

        // Поворот по кругу
        for (int i = 0; i < numObjects; i++)
        {
            // Рассчитать угол поворота для каждого объекта
            float angle = 2 * Mathf.PI * i / numObjects;

            // Рассчитать позицию объекта на окружности
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

            // Создать объект
            GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);

            // Добавить объект в массив
            spawnedObjects[i] = spawnedObject;
        }
    }
}
