using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView m_PhotonView;
    private MousePositionScript mouseController;
    public float movementSpeed = 25f;
    public float rotationSpeed = 10f;
    public float force = 10f;
    public float distanceToStop;

    private bool moving;

    private float currentSpeed = 0;
    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation;

    private float distance;
    private Vector3 moveDir;
    
    private Rigidbody rb;
    private Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
        mouseController = GetComponent<MousePositionScript>();
        m_PhotonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

        //If this script is not on the local player, destroy it.
        if (!m_PhotonView.isMine)
        {
            Destroy(this);
            return;
        }
    }

    void Update()
    {
        currentSpeed = rb.velocity.sqrMagnitude;

        #region Controls for movement
        if (Input.GetKey(KeyCode.Mouse1))
        {
            targetPosition = mouseController.getMouseWorldPoint();
            moving = true;
        }
        #endregion

    }

    void FixedUpdate()
    {
        #region Movement Controls
        if (moving)
        {
            moveDir = targetPosition - transform.position;
            distance = moveDir.sqrMagnitude;

            if (distance < distanceToStop * distanceToStop)
                moving = false;
            else
                FollowTarget(targetPosition, movementSpeed);

        }

        /*if (distance > 0.1f && canMove)
        {
            targetRotation = Quaternion.LookRotation(targetPointMovement - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }*/
        #endregion


    }

    #region Move Towards A target
    void FollowTarget(Vector3 target, float speed)
    {
        rb.AddForce(moveDir.normalized * speed);
    }
    #endregion
}