using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BonusGet : MonoBehaviour
{
    public int requiredClicks = 10;      // ���������� ������ ��� ��������� �����
    private int currentClicks = 0;       // ������� ������� ������
    //public GameObject prize;             // ����, ������� ����� �����
    public float scaleIncrement = 0.1f;  // ���������� ������� ��� �����
    public float fallSpeed = 0.1f;       // �������� ������� �������
    public float rotateSpeed = 5.0f;     // �������� �������� �������

    public int bonusIndex;

    private CircleManager circleManager;

    void Start()
    {
        circleManager = ServiceLocator.GetService<CircleManager>();
        // ���������, ��� � ������� ���� Collider2D
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        // ������ ����
        StartCoroutine(FallDown());
    }

    private void Update()
    {
     //   transform.eulerAngles = transform.eulerAngles+ new Vector3(0, 0, Time.deltaTime * rotateSpeed);
    }

    void OnMouseDown()
    {
        // ����������� ������
        transform.localScale += new UnityEngine.Vector3(scaleIncrement, scaleIncrement, 0.0f);

        currentClicks++;
        Debug.Log("������: " + currentClicks);

        if (currentClicks >= requiredClicks)
        {
            GivePrize();
        }
    }

    void GivePrize()
    {
        Debug.Log("���� �����!");
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
