using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {


    public Sprite laserOnSprite;
    public Sprite laserOffSprite;
 
    public float interval = 0.5f;
    public float rotationSpeed = 0.0f;

    private bool isLaserOn = true;
    private float timeUntilNextToggle;

    private bool canBlink;
    public bool palka = false;

    void Start()
    {
        timeUntilNextToggle = interval;
        if (!palka)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    canBlink = false;
                    break;
                case 1:
                    canBlink = true;
                    break;
            }
            rotationSpeed = Random.Range(0f, 70f);
        }
        else canBlink = false;
    }

    void FixedUpdate()
    {
        timeUntilNextToggle -= Time.fixedDeltaTime;
        if (canBlink && timeUntilNextToggle <= 0)
        {
            isLaserOn = !isLaserOn;           
            GetComponent<BoxCollider2D>().enabled = isLaserOn;
            SpriteRenderer spriteRenderer = ((SpriteRenderer)GetComponent<Renderer>());
            if (isLaserOn)
                spriteRenderer.sprite = laserOnSprite;
            else
                spriteRenderer.sprite = laserOffSprite;
            timeUntilNextToggle = interval;
        }
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
    }
}
