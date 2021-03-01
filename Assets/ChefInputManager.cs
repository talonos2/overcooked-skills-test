using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ChefInputManager : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxAxisInput;
    [SerializeField]
    private float inputLerpSpeed;
    [SerializeField]
    private float interactableDistance = 1.5f;

    private CharacterController characterController;
    private Camera mainCamera;
    private Animator animator;

    private float upness;
    private float rightness;

    private List<Carriable> carried = new List<Carriable>();

    // Start is called before the first frame update

    void Start()
    {
        this.characterController = this.GetComponent<CharacterController>();
        this.mainCamera = Camera.main;
        this.animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(interact))
        {
            float minAngleOff = Mathf.Infinity;
            Interactable thingToInteractWith = null;

            Collider[] nearby = Physics.OverlapSphere(this.transform.position,interactableDistance);
            foreach (Collider c in nearby)
            {
                Interactable interactableThing = c.gameObject.GetComponent<Interactable>();
                if (interactableThing)
                {
                    Vector3 delta = interactableThing.transform.position - this.transform.position;
                    float angleOff = Vector3.Angle(this.transform.forward, delta);
                    if (angleOff<90&&angleOff< minAngleOff) //only 90 degree angle of interaction;
                    {
                        minAngleOff = angleOff;
                        thingToInteractWith = interactableThing;
                    }
                }
            }

            if (thingToInteractWith) //There was at least one thing in a 90 degree angle in front of us.
            {
                if (thingToInteractWith is Carriable&&carried.Count < 2)
                {
                    PickUp((Carriable)thingToInteractWith);
                }
                if (thingToInteractWith is PlaceableArea && carried.Count > 0 && ((PlaceableArea)thingToInteractWith).CanAccept(carried[0]))
                {
                    
                    ReorderHands();
                    ((PlaceableArea)thingToInteractWith).Recieve(Drop());
                }
            }
        }
    }

    private Carriable Drop()
    {
        Carriable thingDropped = carried[0];
        carried.RemoveAt(0);
        thingDropped.transform.parent = null;
        thingDropped.SetColliderEnabled(true);
        return thingDropped;
    }

    private void ReorderHands()
    {
        if (carried.Count>0)
        {
            carried[0].transform.parent = leftHand;
            carried[0].transform.localPosition = Vector3.zero;
        }
        if (carried.Count > 1)
        {
            carried[1].transform.parent = rightHand;
            carried[1].transform.localPosition = Vector3.zero;
        }
    }

    private void PickUp(Carriable thingToPickUp)
    {
        Debug.Log("Picking up!");
        carried.Add(thingToPickUp);
        thingToPickUp.SetColliderEnabled(false);
        ReorderHands();
    }

    private void HandleMovement()
    {
        //With more time, I'd put together something where instead of "up" taking precedence, whichever key
        //the player pressed most recently would have precidence.
        if (Input.GetKey(up))
        {
            upness = Mathf.Lerp(upness, maxAxisInput, inputLerpSpeed);
        }
        else if (Input.GetKey(down))
        {
            upness = Mathf.Lerp(upness, -maxAxisInput, inputLerpSpeed);
        }
        else
        {
            upness = Mathf.Lerp(upness, 0, inputLerpSpeed);
        }

        if (Input.GetKey(right))
        {
            rightness = Mathf.Lerp(rightness, maxAxisInput, inputLerpSpeed);
        }
        else if (Input.GetKey(left))
        {
            rightness = Mathf.Lerp(rightness, -maxAxisInput, inputLerpSpeed);
        }
        else
        {
            rightness = Mathf.Lerp(rightness, 0, inputLerpSpeed);
        }

        if (Mathf.Abs(rightness) < .1f && Mathf.Abs(upness) < .1f)
        {
            animator.SetBool("IsMoving", false);
            animator.SetFloat("MoveSpeed", 1);
            return; //no movement.
        }

        Vector3 toMove = new Vector3();
        toMove += Vector3.right * rightness;

        // Common pitfall: players expect "Up" to be "Up" in screen space, not world space. "Up" in screenspace is "Away from the camera".
        // I forget the math on this, and time is limited, so instead of rederiving it we're faking it here.
        // Convert player coords to screenspace, 
        Vector3 playerCoords = this.transform.position;
        Physics.Raycast(mainCamera.ScreenPointToRay(mainCamera.WorldToScreenPoint(playerCoords) + new Vector3(0, -100f, 0)), out RaycastHit hit, 100f);

        Vector3 upFromPlayer = playerCoords - hit.point;
        toMove += upFromPlayer.normalized * upness;

        //Known bug: You accelerate a little more quickly if you go on both the x/y axis and the up/down axis simultaneously.
        //Acceleration is near-instant anyway, though, so it's not a big deal.


        if (toMove.magnitude > 1)
        {
            toMove.Normalize();
        }

        float angle = Mathf.Rad2Deg * Mathf.Atan2(toMove.x, toMove.z);
        this.transform.rotation = Quaternion.Euler(0, angle, 0);

        animator.SetBool("IsMoving", true);
        animator.SetFloat("MoveSpeed", toMove.magnitude);
    }
}
