using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    public InputActionReference move;
    public InputActionReference talk;
    public InputActionReference interact;
    public InputActionReference showQuestBookAction;

    public QuestManager questManager;

    private NPC currentNPC;
    private Item currentItem;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.Instance.isDialogueActive)
        {
            // stop movement
            rb.linearVelocity = Vector3.zero;
            return;
        }

        if (showQuestBookAction.action.triggered)
        {
            if (questManager.questBook.activeSelf)
            {
                questManager.HideQuestBook();
            }
            else
            {
                questManager.ShowQuestBook();
            }
        }

        if (talk.action.triggered && currentNPC != null)
        {
            currentNPC.Interact();
        }

        if (interact.action.triggered && currentItem != null)
        {
            currentItem.Collect();
            currentItem = null;
        }

        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        Vector2 moveInput = move.action.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        rb.linearVelocity = moveDir * speed;


        if (moveInput.x != 0 && moveInput.x < 0)
        {
            sr.flipX = true;
        }
        else if (moveInput.x != 0 && moveInput.x > 0)
        {
            sr.flipX = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPC>();
            currentNPC.PlayerNearby();
        }

        if (other.CompareTag("Item"))
        {
            currentItem = other.GetComponent<Item>();
            currentItem.PlayerNearby();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (currentNPC != null && other.gameObject == currentNPC.gameObject)
        {
            currentNPC.PlayerAway();
            currentNPC = null;
        }

        if (currentItem != null && other.gameObject == currentItem.gameObject)
        {
            currentItem.PlayerAway();
            currentItem = null;
        }
    }
}
