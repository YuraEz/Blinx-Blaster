using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    //public int life = 1;

    public List<Color> partColor;
    public SpriteRenderer partRenderer;
    public SpriteRenderer outlineRenderer;
    public Color curColor;
    public int layerIndex;
    public int arrayIndex;
    public bool randomChangeColor;

    [SerializeField] private GameObject ability;
    public bool hasAbility;
    [SerializeField][Range(0f, 1f)] private float spawnChance = 0.05f;  // Вероятность появления объекта (5%)

    public int partLife = 1;
    public GameObject partBroke;

    private Coroutine colorChangeCoroutine;

    void OnEnable()
    {
        partRenderer.color = partColor[Random.Range(0, partColor.Count)];
        curColor = partRenderer.color;

        // Генерируем случайное число от 0 до 1
        float randomValue = Random.Range(0f, 1f);

        // Проверяем, меньше ли случайное число заданной вероятности
        if (randomValue < spawnChance)
        {
            // Создаем объект в позиции текущего объекта (this.transform.position)
            ability.SetActive(true);
            hasAbility = true;
        }

        if (randomChangeColor)
        {
            Debug.Log("Starting ChangeColorPeriodically coroutine.");
            colorChangeCoroutine = StartCoroutine(ChangeColorPeriodically());
        }
    }

    void OnDisable()
    {
        // Stop the color changing coroutine if the object is disabled
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }
    }

    public void SetOutlineIndex(int index)
    {
        //outlineRenderer.sortingOrder = index;
    }

    public void SetColor(Color color)
    {
        this.curColor = color;
        partRenderer.color = color;
    }

    private IEnumerator ChangeColorPeriodically()
    {
        while (true)
        {
            // Wait for a random duration between 2 and 5 seconds
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            // Change the color to a random color from the list
            SetColor(partColor[Random.Range(0, partColor.Count)]);
        }
    }
}
