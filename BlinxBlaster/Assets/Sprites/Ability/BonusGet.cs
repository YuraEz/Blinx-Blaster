using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BonusGet : MonoBehaviour
{
    public int requiredClicks = 10;      // Количество кликов для получения приза
    private int currentClicks = 0;       // Текущий счетчик кликов
    //public GameObject prize;             // Приз, который будет выдан
    public float scaleIncrement = 0.1f;  // Увеличение объекта при клике
    public float fallSpeed = 0.1f;       // Скорость падения объекта
    public float rotateSpeed = 5.0f;     // Скорость вращения объекта

    public int bonusIndex;

    private CircleManager circleManager;

    void Start()
    {
        circleManager = ServiceLocator.GetService<CircleManager>();
        // Убедитесь, что у объекта есть Collider2D
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        // Падаем вниз
        StartCoroutine(FallDown());
    }

    private void Update()
    {
     //   transform.eulerAngles = transform.eulerAngles+ new Vector3(0, 0, Time.deltaTime * rotateSpeed);
    }

    void OnMouseDown()
    {
        // Увеличиваем объект
        transform.localScale += new UnityEngine.Vector3(scaleIncrement, scaleIncrement, 0.0f);

        currentClicks++;
        Debug.Log("Кликов: " + currentClicks);

        if (currentClicks >= requiredClicks)
        {
            GivePrize();
        }
    }

    void GivePrize()
    {
        Debug.Log("Приз выдан!");
        Destroy(gameObject);
        if (bonusIndex == 0)
        {
            circleManager.bonus1();
        }
        else if (bonusIndex == 1)
        {
            circleManager.bonus2();
        }
        else if (bonusIndex == 2)
        {
            circleManager.bonus3();
        }
    }

    IEnumerator FallDown()
    {
        while (transform.position.y > -5.0f)
        {
            transform.Translate(UnityEngine.Vector3.down * fallSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
