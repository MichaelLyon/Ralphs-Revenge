using UnityEngine;
using System.Collections;

public static class Functions
{
    static void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public static Vector3 GetWorldPointFromScreenPoint(int screenX, int screenY, float z)
    {
        //Camera.main.ScreenToWorldPoint
        Plane screen = new Plane(Vector3.back, Vector3.zero);// creates a plane at (0,0,0)facing the camera
        Vector2 screenBound = new Vector2(screenX, screenY); // puts aruments into a vector 2 representing a 2d point on the screen.
        Ray ray = Camera.main.ScreenPointToRay(screenBound); //defines a ray through the screen point to a real point
        float hitDist; //to store a hit distance if needed
        if (screen.Raycast(ray, out hitDist))
        { //casts the ray
            screenBound = ray.GetPoint(hitDist); //finds the x and y of the ray in real space
            return new Vector3(screenBound.x, screenBound.y, z);// creates a 3d position using the 2d coordinates at z of of users choice
            //topLeft=new Vector3(screenBound.x,screenBound.y,0);
        }
        else return Vector3.zero;
    }
    public static Vector3 ScreenPointBotLeft //gets a vector 3 of the bottom left of the screen
    {
        get
        {
            return GetWorldPointFromScreenPoint(0, 0, 0);
        }
    }
    public static Vector3 ScreenPointBotRight
    {
        get
        {
            return GetWorldPointFromScreenPoint(Screen.width, 0, 0);
        }
    }
    public static Vector3 ScreenPointTopLeft
    {
        get
        {
            return GetWorldPointFromScreenPoint(0, Screen.height, 0);
        }
    }
    public static Vector3 ScreenPointTopRight
    {
        get
        {
            return GetWorldPointFromScreenPoint(Screen.width, Screen.height, 0);
        }
    }
}
