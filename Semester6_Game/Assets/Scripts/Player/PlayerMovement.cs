using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView m_PhotonView;
    private MousePositionScript mouseController;
    private Animator anim;
    public float movementSpeed = 15f;
    public float rotationSpeed = 10f;
    public float distanceToStop;
    private float originalSpeed;

    public bool moving;

    public float currentSpeed = 0;
    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation;

    private float distance;
    private Vector3 moveDir;

    //Freeze controls
    private bool canPlayerMove = true;
    private float timeStamp = 0;
    private float slowAmount;
    private float slowDuration;

    private Rigidbody rb;


    void Awake()
    {
        anim = GetComponent<Animator>();
        mouseController = GetComponent<MousePositionScript>();
        m_PhotonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

        originalSpeed = movementSpeed;
        //If this script is not on the local player, destroy it.
        if (!m_PhotonView.isMine)
        {
            Destroy(this);
            return;
        }
    }

    void FixedUpdate()
    {
        if (canPlayerMove)
        {
            currentSpeed = rb.velocity.sqrMagnitude;
        }
        else
        {
            currentSpeed = 0;
        }
        anim.SetFloat("m_MoveSpeed", currentSpeed);
        #region Key pressed action
        if (Input.GetKey(KeyCode.Mouse1))
        {
            targetPosition = mouseController.getMouseWorldPoint();
            moving = true;
        }
        #endregion

        if (canPlayerMove)
        {
            #region Movement
            if (moving)
            {
                moveDir = targetPosition - transform.position;
                distance = moveDir.sqrMagnitude;

                if (distance < distanceToStop * distanceToStop)
                    moving = false;
                else
                    FollowTarget(targetPosition, movementSpeed);

            }

            if (distance > 0.1f && moving)
            {
                targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            #endregion
        }

        if(Time.time >= timeStamp + slowDuration)
        {
            movementSpeed = originalSpeed;
        }
    }

    #region Move Towards A target
    void FollowTarget(Vector3 target, float speed)
    {
        rb.AddForce(moveDir.normalized * speed);
    }
    #endregion

    public bool FreezePlayerMovement()
    {
        return canPlayerMove = false;
    }

    public bool UnfreezePlayerMovemenet()
    {
        return canPlayerMove = true;
    }

    public void slowPlayerMovementSpeed(float _slowMovementSpeed, float _slowDuration)
    {
        movementSpeed = _slowMovementSpeed;
        slowDuration = _slowDuration;
        timeStamp = Time.time;
    }
}