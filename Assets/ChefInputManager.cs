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

    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxAxisInput;
    [SerializeField]
    private float inputLerpSpeed;

    private CharacterController characterController;
    private Camera mainCamera;
    private Animator animator;

    private float upness;
    private float rightness;
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

        if (Mathf.Abs(rightness)<.1f&& Mathf.Abs(upness) <.1f)
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

        Debug.Log(hit.point+", "+playerCoords);

        Vector3 upFromPlayer = playerCoords - hit.point;
        toMove += upFromPlayer.normalized * upness;

        //Known bug: You accelerate a little more quickly if you go on both the x/y axis and the up/down axis simultaneously.
        //Acceleration is near-instant anyway, though, so it's not a big deal.


        if (toMove.magnitude > 1)
        {
            toMove.Normalize();
        }

        float angle = Mathf.Rad2Deg*Mathf.Atan2(toMove.x, toMove.z);
        this.transform.rotation = Quaternion.Euler(0, angle, 0);

        animator.SetBool("IsMoving", true);
        animator.SetFloat("MoveSpeed", toMove.magnitude);
    }
}
