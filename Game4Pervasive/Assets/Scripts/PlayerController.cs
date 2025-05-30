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
    public InputActionReference showMenuAction;

    public MenuManager menuManager;

    private NPC currentNPC;
    private Item currentItem;
    public Animator animator;

    public GameObject rainEffect;
    public GameObject snowEffect;
    public GameObject lightningEffect;
    public GameObject fogEffect;

    public InputActionReference rain;
    public InputActionReference snow;
    public InputActionReference lightning;
    public InputActionReference fog;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // initialize the inventory
        // InitInv();
    }

    // TODO remove this - this is temp for testing purposes
    private void InitInv()
    {
        // get all objects with item tags
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        // loop through all items and add them to the inventory
        foreach (GameObject item in items)
        {
            Item itemComponent = item.GetComponent<Item>();
            if (itemComponent != null)
            {
                Inventory.Instance.AddItem(itemComponent.itemData);
            }
        }
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

        if (showMenuAction.action.triggered)
        {
            if (menuManager.menu.activeSelf)
            {
                menuManager.HideMainMenu();
            }
            else
            {
                menuManager.ShowMainMenu();
            }
        }

        // if we are in menu don't move
        if (menuManager.menu.activeSelf)
        {
            rb.linearVelocity = Vector3.zero;
            return;
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
        if (moveInput == Vector2.zero)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", true);
        }

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


        // Weather effects
        if (rain.action.triggered)
        {
            rainEffect.SetActive(!rainEffect.activeSelf);
        }
        if (snow.action.triggered)
        {
            snowEffect.SetActive(!snowEffect.activeSelf);
        }
        if (lightning.action.triggered)
        {
            lightningEffect.SetActive(!lightningEffect.activeSelf);
        }
        if (fog.action.triggered)
        {
            fogEffect.SetActive(!fogEffect.activeSelf);
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
