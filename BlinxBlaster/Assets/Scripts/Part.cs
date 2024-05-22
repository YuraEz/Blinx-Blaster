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

    [SerializeField] private GameObject ability;
    public bool hasAbility;
    [SerializeField][Range(0f, 1f)] private float spawnChance = 0.05f;  // Вероятность появления объекта (5%)

    public int partLife = 1;
    public GameObject partBroke;

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
}
