using UnityEngine;
using System.Collections;

public class MotorCycleBoss : BaseEnemy 
{
    public GameObject childLeft;
    public GameObject childRight;
    public GameObject HealthBar;
    private HealthBarScript healthBarScript;
    private bool hasHealthBar = false;

    //used in editor, enter time you want in seconds. that will be converted to time in updates.
    public float TimeStraight=0;
    public float TimeTurning=0;
    public float TimeCoolDown = 0;

    //used for attack logic.
    //the bools are used to make sure that attacks were set.
    //nextAttack decides what the current attack will be, defaults to one.
    //first attack is not implemented yet, jsut to make the attack method more stable, so that you don't need input the objects in order.
    //last attack is incremented during start() for each attack added in the editor. it is used to make sure attack roles over.
    //NOTE,  FOR RIGHT NOW THE ATTACK PATTERN NEEDS TO BE SET AT CALTROP, SMOKESCREEN, OILSLICK JUST TRUST ME ON THIS
    public GameObject AttackOne;
    public GameObject AttackTwo;
    public GameObject AttackThree;
    private bool attackOnePresent = false;
    private bool attackTwoPresent = false;
    private bool attackThreePresent = false;
    private bool attackReady = false;
    private int nextAttack = 1;
    private int firstAttack = 1;
    private int lastAttack = 0;

    //player refference
    private GameObject target;
    //range of attack, based on scale and position. x is left side y is right.
    private Vector2 xRange;

    //private:
    //timeSpent in any direction, will use the public vars * 60;
    private int straightLength;
    private int vertLength;
    public int coolDownLength;

    //counters
    private int moveCounter=0;
    public int attackCounter=0;

    //if set to true at first
    //-motorcycle will follow this pattern
    //s/r/s/l/s/l/s/r-repeat
    //-else
    //s/l/s/r/s/r/s/l-repeat
    public bool goRight = true;
    //decides if its been the second turn. you should leave this alone for now.
    public bool secondPass = true;
    //decides if motorcycle will move in a straightline.
    //left private right now for testing. if you're reading this let me know.
    private bool goForward = true;

    //directional Vectors made here so not to break garabe collector
    //FYI left and right are from the players perspective not the units
    private Vector3 forward;
    private Vector3 diagRight;
    private Vector3 diagLeft;


	// Use this for initialization
	void Start ()
    {
        base.Start();
        if(HealthBar != null)
        {
            GameObject bar = (GameObject)Object.Instantiate(HealthBar, transform.position, Quaternion.identity);
            //healthBarScript = HealthBar.GetComponent<HealthBarScript>();
            healthBarScript = bar.GetComponent<HealthBarScript>();
            hasHealthBar = true;
        }
        //first attack was set in editor
        if(AttackOne != null)
        {
            attackOnePresent = true;
            
            firstAttack = 1;
            lastAttack++;
        }
        //second attack is set in editor
        if(AttackTwo != null)
        {
            attackTwoPresent = true;
            lastAttack++;
        }
        //third attack is set in editor
        if(AttackThree != null)
        {
            attackThreePresent = true;
            lastAttack++;
        }
        //assigns actual update time, using the time variables * 60;
        straightLength = (int)(TimeStraight * 60f);
        vertLength = (int)(TimeTurning * 60f);
        coolDownLength = (int)(TimeCoolDown * 60f);
        //sets directional vectors, this way I dont have to continually make new ones over the course of the game.
        forward = new Vector3(0f, 1f, 0f);
        diagRight = new Vector3(1f, 1f, 0f);
        diagLeft = new Vector3(-1f, 1f, 0f);
        //finds and sets the player object as target, used so I can check if I want to attack.
        target = GameObject.FindGameObjectWithTag("Player");
        //instantiates xRange
        xRange = new Vector2();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //this order must be preserved since xRange will use current position and attack will check xRange.
        move();
        base.Update();
        reAssignXRange();
        attack();
        //checks if it has a healthbar, if you want you can completly skip health bars.
        if (hasHealthBar)
        {
            healthBarScript.Update(startingHitPoints, hitPoints, transform.position.x, transform.position.y);
        }
	}
    
    //run once an update after movement.
    private void reAssignXRange()
    {
        xRange.x = gameObject.transform.position.x - (gameObject.transform.localScale.x / 2f);
        xRange.y = gameObject.transform.position.x + (gameObject.transform.localScale.x / 2f);
    }

    //handles movement
    private void move()
    {
        if(goForward)
        {
            if(moveCounter <= straightLength)
            {
                direction = forward;
                moveCounter++;
                
                if(moveCounter > straightLength)
                {
                    resetMoveCounter();
                    goForward = false;
                }
            }
        }
        else
        {
            if(goRight)
            {
                if(moveCounter <= vertLength)
                {
                    direction = diagRight;
                    moveCounter++;
                    if(moveCounter > vertLength)
                    {
                        goForward = true;
                        resetMoveCounter();
                        if(secondPass)
                        {
                            goRight = false;
                            secondPass = false;
                        }
                        else
                        {
                            secondPass = true;
                        }
                    }
                }
            }
            else
            {
                if(moveCounter <= vertLength)
                {
                    direction = diagLeft;
                    moveCounter++;
                    if(moveCounter > vertLength)
                    {
                        goForward = true;
                        resetMoveCounter();
                        if(secondPass)
                        {
                            goRight = true;
                            secondPass = false;
                        }
                        else
                        {
                            secondPass = true;
                        }
                    }
                }
            }
        }
    }
    private void attack()
    {
        //if no attacks were set then this function does nothing.
        if (attackOnePresent || attackTwoPresent || attackThreePresent)
        {
            //if you are still cooling down increment attack counter, if that makes attackcounter and cooldown length
            //equivilent set attackReady to true.
            if (!attackReady)
            {
                attackCounter++;
                if (attackCounter >= coolDownLength)
                {
                    attackReady = true;
                }
            }
            else
            {
                //if the player is within the same x area as THIS decide which attack to use.
                //current attacks are chosen using the nextAttack variable. when an attack is used
                //nextAttack is incremented. when that increment is higher than maxAttack reset next attack to 1.
                if(checkIfTargetIsWithinRange())
                {
                    if (nextAttack == 1)
                    {
                        if (attackOnePresent)
                        {
                            GameObject.Instantiate(AttackOne,gameObject.transform.position,Quaternion.identity);
                        }
                        nextAttack++;
                        
                    }
                    else if (nextAttack == 2)
                    {
                        if (attackTwoPresent)
                        {
                            GameObject.Instantiate(AttackTwo,gameObject.transform.position,Quaternion.AngleAxis(180,new Vector3(0,1,0)));
                        }
                        nextAttack++;
                        
                    }
                    else if (nextAttack == 3)
                    {
                        if (attackThreePresent)
                        {
                            GameObject.Instantiate(AttackThree, gameObject.transform.position, Quaternion.identity);
                        }
                        nextAttack++;
                        
                    }
                    if (nextAttack > lastAttack)
                    {
                        nextAttack = 1;
                    }
                    attackReady = false;
                    attackCounter = 0;
                    
                }
            }
        }
    }
    private bool checkIfTargetIsWithinRange()
    {
        //check if the player take sup the same 1-d space as THIS on the x-axis.
        if(target.transform.position.x >= xRange.x && target.transform.position.x <= xRange.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        if(hitPoints <= 0)
        {
            Debug.Log("i got here");
           // Object.Instantiate(childLeft,transform.position+new Vector3(-.5f,0,0),Quaternion.identity);
            GameObject childL = (GameObject)GameObject.Instantiate(childLeft);
            childL.transform.position = transform.position;
            childL.SendMessage("BecomeActive");
            //MotorCycleChild childLScript = childL.GetComponent<MotorCycleChild>();
            //Object.Instantiate(childRight, transform.position + new Vector3(.5f, 0, 0), Quaternion.identity);
            GameObject childR = (GameObject)GameObject.Instantiate(childRight);
            childR.transform.position = transform.position;
            childR.SendMessage("BecomeActive");
            healthBarScript.die();
            hasHealthBar = false;
        }
        base.OnTriggerEnter(other);
    }

    public void resetMoveCounter()
    {
        moveCounter = 0;
    }
    public void resetCoolDown()
    {
        attackCounter = 0;
    }


}
