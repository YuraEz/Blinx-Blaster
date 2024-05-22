using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class айу : MonoBehaviour
{
    public GameObject[] layers; // массив слоев колеса, каждый слой содержит 10 секторов
    public GameObject sectorPrefab; // префаб сектора
    public float rotationSpeed = 50f; // скорость вращения колеса
    private float timeCounter = 0f;
    private bool clockwise = true;

    void Update()
    {
        RotateWheel();
        ChangeRotationDirection();
    }

    void RotateWheel()
    {
        float direction = clockwise ? 1 : -1;
        transform.Rotate(Vector3.forward, rotationSpeed * direction * Time.deltaTime);
    }

    void ChangeRotationDirection()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= 10f)
        {
            clockwise = !clockwise;
            timeCounter = 0f;
        }
    }

    public void CheckHit(GameObject sector, Color ballColor)
    {
        Color sectorColor = sector.GetComponent<SpriteRenderer>().color;
        if (sectorColor == ballColor)
        {
            sector.SetActive(false); // или Destroy(sector);
            // начисление очков
        }
        else
        {
            AddNewSector(sector.transform.position, sectorColor);
        }
    }

    void AddNewSector(Vector3 hitPosition, Color color)
    {
        // логика добавления нового сектора над сектором, в который попали
        foreach (GameObject layer in layers)
        {
            foreach (Transform sector in layer.transform)
            {
                if (!sector.gameObject.activeInHierarchy)
                {
                    sector.gameObject.SetActive(true);
                    sector.GetComponent<SpriteRenderer>().color = color;
                    sector.position = hitPosition + new Vector3(0, 0, -1); // поместить над сектором
                    return;
                }
            }
        }
    }
}
