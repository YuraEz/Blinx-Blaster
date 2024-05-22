using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class CircleManager : MonoBehaviour
{
    public int partsAmount;
    public int startLayersAmount;
    public int maxLayersAmount;

    public float multiplierScale = 0.3f;
    public float increaseDe = 2f;
    public float padding = 0.1f;
    public float startScale = 0.1f;
    public float partsRotation = 30;
    public GameObject partPref;
    public Dictionary<int, Part[]> Parts;

    public float rotateSpeed = 3;
    public float rotateAbility;
    public float rotateStopDelay = 3f;

    public List<GameObject> scaleList;
    public float scaleDelay = 10f;

    public float updatedScoreValue = 25f;

    public static CircleManager instance;


    private UIManager uiManager;
    public ScoreManager scoreManager;

    public bool useSnipeMode = false;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        uiManager = ServiceLocator.GetService<UIManager>();
        instance = this;

        if (useSnipeMode) ScaleOn();

        Parts = new Dictionary<int, Part[]>();
        for (int i = 0; i < maxLayersAmount; i++)
        {
            if (!Parts.ContainsKey(i))
            {
                Parts.Add(i, new Part[partsAmount]);
            }
            for (int j = 0; j < partsAmount; j++)
            {
                GameObject part = Instantiate(partPref, transform.position, Quaternion.identity, transform);
                part.transform.eulerAngles = new Vector3(0, 0, j * partsRotation);
                Vector3 newScale = new Vector3(i * multiplierScale, i * multiplierScale, i * multiplierScale);
                
                if (i > 0)
                {
                    part.transform.localScale = (partPref.transform.localScale * increaseDe) + (newScale);
                }
                else
                {
                    part.transform.localScale = partPref.transform.localScale * startScale;
                }

                SpriteRenderer spriteRenderer = part.GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.sortingOrder = -i;
                Part partObj = part.GetComponent<Part>();
                partObj.SetOutlineIndex(-i * 2);
                Parts[i][j] = partObj;
                partObj.layerIndex = i;
                partObj.arrayIndex = j;
                partObj.gameObject.SetActive(i <= startLayersAmount);     
            }
        }
      
    }

    private float timeCounter = 0f;  // Счётчик времени
    private bool rotateClockwise = true; // Флаг для направления вращения
    public TextMeshProUGUI timerText; // Ссылка на TextMesh Pro компонент для отображения времени
    private float timerValue = 10f; // Значение таймера

    private void Update()
    {
        //transform.eulerAngles = transform.eulerAngles+ new Vector3(0, 0, Time.deltaTime * rotateSpeed);
        // Обновляем счётчик времени
        timeCounter += Time.deltaTime;

        // Уменьшаем значение таймера каждую секунду
        if (timeCounter >= 1f)
        {
            timerValue -= 1f;
            timeCounter = 0f;
        }

        // Обновляем текст таймера
        //timerText.text = timerValue.ToString("0");
        timerText.text = "00:" + timerValue.ToString("00");

        // Проверяем, если таймер достиг 0
        if (timerValue <= 0f)
        {
            // Сбрасываем значение таймера на 10
            timerValue = 10f;

            // Переключаем направление вращения
            rotateClockwise = !rotateClockwise;
        }

        // Вычисляем текущий угол вращения
        float rotationDirection = rotateClockwise ? 1 : -1;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Time.deltaTime * rotateSpeed * rotationDirection);
    }

    public void AddPart(Part collidedPart, Color color)
    {
        if (Parts.Count <= (collidedPart.layerIndex + 1))
        {
            //lose
            scoreManager.Finish(false);
            uiManager.ChangeScreen("lose");      
            return;
        }

        Part part = Parts[collidedPart.layerIndex + 1][collidedPart.arrayIndex];
        part.gameObject.SetActive(true);
        part.SetColor(color);
    }


    public List<GameObject> abilityPrefs;
    public void UseAbility(Vector3 vector3)
    {
        int randomIndex = UnityEngine.Random.Range(0, 3); // Генерируем случайное число от 0 до 2

        if (randomIndex == 0)
        {
            GameObject instance = Instantiate(abilityPrefs[0], vector3, Quaternion.identity);
        }
        else if (randomIndex == 1)
        {
            GameObject instance = Instantiate(abilityPrefs[1], vector3, Quaternion.identity);
        }
        else if (randomIndex == 2)
        {
            GameObject instance = Instantiate(abilityPrefs[2], vector3, Quaternion.identity);
        }
    }

    public void bonus1()
    {
        rotateSpeed = 0;
        Invoke("StartRotate", rotateStopDelay);
        PlayerPrefs.SetInt("goalamount2", PlayerPrefs.GetInt("goalamount2", 0) + 1);
    }

    public void bonus2()
    {
        ScaleOn();
        Invoke("ScaleOff", scaleDelay);
        PlayerPrefs.SetInt("goalamount5", PlayerPrefs.GetInt("goalamount5", 0) + 1);
    }

    public void bonus3()
    {
        scoreManager.UpdateGame((int)updatedScoreValue);
    }

    void StartRotate()
    {
        rotateSpeed = rotateAbility;
    }


    void ScaleOn()
    {
        foreach (GameObject scale in scaleList)
        {
            scale.SetActive(true);
        }
    }

    void ScaleOff()
    {
        if (useSnipeMode) return;
        foreach (GameObject scale in scaleList)
        {
            scale.SetActive(false);
        }
    }
}
