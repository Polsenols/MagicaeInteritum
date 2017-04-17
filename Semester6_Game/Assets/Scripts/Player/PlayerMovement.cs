using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView m_PhotonView;
    private MousePositionScript mouseController;
    private Animator anim;
    private MoveIndicatorController moveIndicator;

    public float movementSpeed = 15f;
    public float rotationSpeed = 10f;
    public float distanceToStop;
    private float originalSpeed;

    public bool moving;

    TeleportToShop _teleportToShop;

    public float currentSpeed = 0;
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 targetPosRotation;
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
        mouseController = GetComponent<MousePositionScript>();
        m_PhotonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        _teleportToShop = GetComponent<TeleportToShop>();
        moveIndicator = GetComponent<MoveIndicatorController>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
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
            if (_teleportToShop.teleportingToShop)
                _teleportToShop.StopPlayerRecall();
            targetPosition = mouseController.getMouseWorldPoint();
            targetPosRotation = targetPosition;
            moving = true;
            anim.SetBool("Cast", false);

            //Update MoveIndicator
            moveIndicator.UpdateMoveIndicator(targetPosition);
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
                RotateToPos();
            }
;
            #endregion
        }

        if (Time.time >= timeStamp + slowDuration)
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

    private void RotateToPos()
    {
        targetPosRotation.y = 0;
        Vector3 myPos = transform.position;
        myPos.y = 0;
        targetRotation = Quaternion.LookRotation(targetPosRotation - myPos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }

    public bool FreezePlayerMovement()
    {
        return canPlayerMove = false;
    }

    public bool UnfreezePlayerMovemenet()
    {
        return canPlayerMove = true;
    }

    public void SetTargetRotationPos(Vector3 pos)
    {
        targetPosRotation = pos;
    }

    public void slowPlayerMovementSpeed(float _slowMovementSpeed, float _slowDuration)
    {
        movementSpeed = _slowMovementSpeed;
        slowDuration = _slowDuration;
        timeStamp = Time.time;
    }
}