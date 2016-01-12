using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }
    public Text partner;
    public GameObject coinVisual;
    float speed = 2;
   public float rotationSpeed = 35;
    public SpriteRenderer renderer;
    public Vector3 direction;
    float downspeed = 0;
    // Update is called once per frame
    void Update()
    {

        if (renderer.color.a > .1f)
        {
            downspeed += .05f;
            transform.position += Vector3.down * downspeed * Time.deltaTime;
            speed -= 3f * Time.deltaTime;
            rotationSpeed *= .99f;
            coinVisual.transform.Rotate(0, rotationSpeed, 0);
            transform.position += direction * speed * Time.deltaTime;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.g, renderer.color.a * .99f);
        }
        else
        {
            // renderer.color = Color.clear;
            gameObject.SetActive(false);
        }
    }

    void Reset(Vector3 position)
    {
        downspeed = 0;
        gameObject.SetActive(true);
        transform.position = position;
        speed = 3;
        renderer.color = Color.white;
        rotationSpeed = 20;
    }

}
