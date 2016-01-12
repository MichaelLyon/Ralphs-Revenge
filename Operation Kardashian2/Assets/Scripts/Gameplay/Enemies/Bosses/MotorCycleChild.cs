using UnityEngine;
using System.Collections;

public class MotorCycleChild : BaseEnemy {
    public enum childClass { left, right };
    public enum childBehavior { driving, spinOut };

    public childClass swerveDir = childClass.left;
    public float timeBeforeSpinOut = 1f;
    public float spinOutTime = 2f;
    public float spinOutDegree = 6f;

    private childBehavior currentBehavior = childBehavior.driving;
    private float swerveAngle = 45f;
    private int swerveCount;
    private int spinOutCount;

    private int behaviorCount = 0;

	// Use this for initialization
	void Start () 
    {
        base.Start();
        alive = true;
        swerveCount = (int)(timeBeforeSpinOut * 60f);
        spinOutCount = (int)(spinOutTime * 60f);
        calcDirection();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(currentBehavior == childBehavior.driving)
        {
            checkToSpinOut();
        }
        if(currentBehavior == childBehavior.spinOut)
        {
            spin();
        }
        base.Update();
	}

    void calcDirection()
    {
        //note for right now the characters go at
        //direct 45 degree angles. this will be changed
        //to match an angle if necessary

        if(swerveDir == childClass.right)
        {
            direction = new Vector3(1f, 1f, 0f);
        }
        if(swerveDir == childClass.left)
        {
            direction = new Vector3(-1f, 1f, 0f);
        }
    }

    void checkToSpinOut()
    {
        if(behaviorCount >= swerveCount)
        {
            currentBehavior = childBehavior.spinOut;
            behaviorCount = 0;
        }
        else
        {
            behaviorCount++;
        }
    }

    void spin()
    {
        if(behaviorCount >= spinOutCount)
        {
            direction = Vector3.zero;
            hitPoints = 1;
            //put death here.
        }
        else
        {
            if(swerveDir == childClass.left)
            {
                gameObject.transform.Rotate(0, 0, spinOutDegree);
            }
            else
            {
                gameObject.transform.Rotate(0, 0, -spinOutDegree);
            }
            behaviorCount++;
        }
    }

    public void makeAlive()
    {
        alive = true;
    }
}
