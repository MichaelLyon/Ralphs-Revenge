using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour 
{
    private float xScale;
    private Vector3 tempScale;
    private Vector3 tempPos;
    public Vector2 Positioning;

	// Use this for initialization
	void Start () 
    {
        xScale = 1f;
        tempScale = new Vector3(1f, 1f, 1f);
        tempPos = new Vector3(1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void Update(int startHealth, int CurrentHealth, float posX, float posY)
    {
        xScale = (float)CurrentHealth / (float)startHealth;
        tempScale.x = xScale;
        tempScale.y = transform.localScale.y;
        tempScale.z = transform.localScale.z;
        transform.localScale = tempScale;
        Move(posX, posY);
    }

    private void Move(float posX, float posY)
    {
        tempPos.x = posX + Positioning.x;
        tempPos.y = posY + Positioning.y;
        transform.position = tempPos;
    }

    public void die()
    {
        GameObject.Destroy(gameObject);
    }
}
