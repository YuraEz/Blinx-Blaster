using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject ballPref;
    [SerializeField] private float shotForece;
    [SerializeField] private Button shotBtn;
    [SerializeField] private Ball curBall;

    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [SerializeField] private List<Color> colors;
    [SerializeField] private List<Sprite> colorSprites;
    [SerializeField] private int colorIndex;

    [SerializeField] private float shotDelay;
    private bool canShoot = true;
    void Start()
    {
        shotBtn.onClick.AddListener(Shot);
        PlaceNewBall();
    }

    public void PlaceNewBall()
    {
        if (curBall)
        {
            Destroy(curBall.gameObject);
        }

        curBall = Instantiate(ballPref, point1.position, Quaternion.identity).GetComponent<Ball>();
        curBall.GetComponent<Collider2D>().isTrigger = true;
        int index = colorIndex;//Random.Range(0, colors.Count);
        curBall.SetColor(colors[index]);
        curBall.GetComponent<SpriteRenderer>().sprite = colorSprites[index];
    }

    private void Shot()
    {
        if (!canShoot) return;
        GameObject newBall = Instantiate(ballPref, point1.position, Quaternion.identity);
        Vector2 dir = point2.position - point1.position;
        newBall.GetComponent<Ball>().SetColor(curBall.color);
        newBall.GetComponent<SpriteRenderer>().sprite = curBall.GetComponent<SpriteRenderer>().sprite;

        newBall.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shotForece, ForceMode2D.Impulse);
        Destroy(newBall, 5);
        PlaceNewBall();
        canShoot = false;
        shotBtn.interactable = false;
        Invoke("CanShot", shotDelay);
    }

    private void CanShot()
    {
        canShoot = true;
        shotBtn.interactable = true;
    }
}
