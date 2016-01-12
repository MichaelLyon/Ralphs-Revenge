using UnityEngine;
using System.Collections;

public class TurningEnemy : BaseEnemy
{

    /***********************************************
     * MELEE ENEMY
    /**********************************************/
    /**********************************************
     * Behavior is setable in unity and will choose
     * if the enemy will run off the screen or try
     * to realign itself with the player and turn
     * around.
     * Set to default straight to not break melee
     * units already made.
    /*********************************************/
    public enum Behavior { Straight, Curve };
    public Behavior behavior = Behavior.Straight;

    /*********************************************
     * Dir is used in the curved move function to
     * determine which direction it should go.
     * if behavior is straight this is ignored
     * 
     * 
    /********************************************/
    enum dir { down, up, arcUp, arcDown };
    dir currentDirection = dir.down;

    /*********************************************
     * passDistance: The distance that this can
     * pass the player before this will turn around
    /********************************************/
    public float passDistance;

    /*********************************************
     * Player object, only assigned at start.
    /********************************************/
    GameObject Player;

    /*********************************************
     * Calculated everytime CheckForPass is true
     * used in the curve function to know the Diameter
     * Length DO.NOT.ASSIGN.ANY.OTHER.TIME.
    /**********************************************/
    public float XDistanceFromPlayer;

    /**********************************************
     * Angle and speed used in curve functions.
     * calculated at the same time as diameter
    /*********************************************/
    float CurveAngle;
    float CurveSpeed;

    /*********************************************
     * controlls how long the curve takes in frames.
     * do not fuck with curveint, its the counter used
     * in the curve functions
    /********************************************/
    public int CurveCount = 60;
    int curveInt = 0;

    /********************************************
     * used to control how extreme the curve is.
    /*******************************************/
    public float CurveMult = 1;

    /********************************************
     * determines which way this should turn,
     * is assigned in assign circle
     * turn is based on the players POV
    /*******************************************/
    enum TurnDirection { left, right };
    TurnDirection turn;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3((float)(Random.Range(Functions.ScreenPointTopLeft.x + .4f, Functions.ScreenPointTopRight.x - .4f)), Functions.ScreenPointTopLeft.y, 0);
        direction = Vector3.down;
        speed = 2;
        startingHitPoints = 2;
        Player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (behavior == Behavior.Straight) { base.Update(); }
        else if (behavior == Behavior.Curve)
        {
            CurveUpdate();
        }

    }
    void CurveUpdate()
    {
        if (currentDirection == dir.up || currentDirection == dir.down)
        {
            MoveStraight();
        }
        else if (currentDirection == dir.arcDown)
        {
            CurveDown();
        }
        else if (currentDirection == dir.arcUp)
        {
            CurveUp();
        }
        CheckForDeath();
    }
    bool CheckForPass()
    {
        if (currentDirection == dir.down)
        {
            if (transform.position.y < Player.transform.position.y)
            {
                if ((Player.transform.position.y - transform.position.y) >= passDistance)
                {
                    return true;
                }
            }
        }
        else if (currentDirection == dir.up)
        {
            if (transform.position.y > Player.transform.position.y)
            {
                if ((transform.position.y - Player.transform.position.y) >= passDistance)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void MoveStraight()
    {
        if (currentDirection == dir.down)
        {
            transform.position -= new Vector3(0f, (float)speed, 0f) * Time.deltaTime;
        }
        else if (currentDirection == dir.up)
        {
            transform.position += new Vector3(0f, (float)speed, 0f) * Time.deltaTime;
        }
        if (CheckForPass())
        {
            if (currentDirection == dir.up)
            {
                currentDirection = dir.arcDown;
                AssignCircle();
            }
            if (currentDirection == dir.down)
            {
                currentDirection = dir.arcUp;
                AssignCircle();
            }
        }
    }

    //assigns XdistanceFromPLayer with the absolute distance from player,
    //also creates the angle, speed, and turn direction
    //also sets curveInt to 0
    void AssignCircle()
    {
        curveInt = 0;
        XDistanceFromPlayer = Player.transform.position.x - transform.position.x;
        if (XDistanceFromPlayer < 0)
        {
            XDistanceFromPlayer *= -1;
        }
        CurveAngle = 180 / CurveCount;
        CurveSpeed = ((XDistanceFromPlayer * 3.14f) / 2f) / CurveCount;
        if (transform.position.x < Player.transform.position.x)
        {
            turn = TurnDirection.right;
        }
        else if (transform.position.x > Player.transform.position.x)
        {
            turn = TurnDirection.left;
        }
    }

    //curves to turn up
    void CurveUp()
    {
        if (curveInt < CurveCount)
        {
            if (turn == TurnDirection.left)
            {
                transform.Rotate(0f, 0f, -CurveAngle);
            }
            else if (turn == TurnDirection.right)
            {
                transform.Rotate(0f, 0f, CurveAngle);
            }
            transform.position += (CurveMult * (transform.right * CurveSpeed)) * Time.deltaTime;
            curveInt++;
        }
        else if (curveInt == CurveCount)
        {
            currentDirection = dir.up;
        }

    }

    //curves to turn down
    void CurveDown()
    {
        if (curveInt < CurveCount)
        {
            if (turn == TurnDirection.left)
            {
                transform.Rotate(0f, 0f, CurveAngle);
            }
            else if (turn == TurnDirection.right)
            {
                transform.Rotate(0f, 0f, -CurveAngle);
            }
            transform.position += (CurveMult * (transform.right * CurveSpeed)) * Time.deltaTime;
            curveInt++;
        }
        else if (curveInt == CurveCount)
        {
            currentDirection = dir.down;
        }

    }

    void CheckForDeath()
    {
        if (hitPoints <= 0)
        {
            if (powerUpChance <= powerUpPercentChance && powerUp != null)
            {
                GameObject.Instantiate(powerUp, transform.position, Quaternion.identity);
            }
            uiScore.AddScore(scoreValue);
            gameObject.SetActive(false);
        }
    }
}
