using UnityEngine;

public class NPC : MonoBehaviour
{
    public CharacterLoader characterLoader;
    public string characterFileName;
    public GameObject interactionPrompt;
    public float walkSpeed = 2f;
    public float walkRadius = 5f;
    public float idleTime = 2f;
    public float groundDist = 0.1f;
    public LayerMask terrainLayer;

    private CharacterDataObj characterData;
    public Animator animator;
    private Rigidbody rb;
    private SpriteRenderer sr;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float idleTimer = 0f;
    private bool playerNearby = false;
    public float xPos = 0.3f; // Position offset for interaction prompt

    private void Start()
    {
        characterData = characterLoader.LoadCharacter(characterFileName);
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>(); // Get the SpriteRenderer
        originalPosition = transform.position;
        PickNewDestination();
    }

    private void Update()
    {
        // Stop if in dialogue
        if (DialogueManager.Instance.isDialogueActive || playerNearby)
        {
            rb.linearVelocity = Vector3.zero;
            animator.SetBool("Walk", false);
            return;
        }

        if (DialogueManager.Instance.isDialogueActive)
        {
            interactionPrompt.SetActive(false);
        }

        // Terrain alignment (same as player logic)
        RaycastHit hit;
        Vector3 castPos = transform.position + Vector3.up;
        if (Physics.Raycast(castPos, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            Vector3 movePos = transform.position;
            movePos.y = hit.point.y + groundDist;
            transform.position = movePos;
        }

        // Movement logic
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 1f)
        {
            animator.SetBool("Walk", false);
            rb.linearVelocity = Vector3.zero;
            idleTimer += Time.deltaTime;
            if (idleTimer > idleTime)
            {
                PickNewDestination();
                idleTimer = 0f;
            }
        }
        else
        {
            Vector3 dir = (targetPosition - transform.position).normalized;
            rb.linearVelocity = new Vector3(dir.x, rb.linearVelocity.y, dir.z) * walkSpeed;
            animator.SetBool("Walk", true);

            // Flip sprite
            if (dir.x != 0)
            {
                sr.flipX = dir.x < 0;
            }
        }
    }

    private void PickNewDestination()
    {
        Vector2 randomCircle;
        Vector3 potentialTarget;

        do
        {
            randomCircle = Random.insideUnitCircle * walkRadius;
            potentialTarget = originalPosition + new Vector3(randomCircle.x, 0, randomCircle.y);
        }
        while (potentialTarget.x < 13 || potentialTarget.x > 89 || potentialTarget.z < 5 || potentialTarget.z > 36);

        targetPosition = potentialTarget;
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(characterData);
    }

    public void PlayerNearby()
    {
        playerNearby = true;
        Vector3 direction = (targetPosition - transform.position).normalized;
        interactionPrompt.GetComponent<SpriteRenderer>().flipX = direction.x > 0;
        interactionPrompt.transform.localPosition = new Vector3(direction.x < 0 ? -xPos : xPos, interactionPrompt.transform.localPosition.y, interactionPrompt.transform.localPosition.z);
        interactionPrompt.SetActive(true);
        rb.linearVelocity = Vector3.zero;
    }

    public void PlayerAway()
    {
        playerNearby = false;
        interactionPrompt.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, walkRadius);
    }

    public int GetCharacterApprovalPoints()
    {
        Debug.Log("Getting approval points for character: " + characterData.characterName);
        return characterData.approvalPoints;
    }

    public CharacterDataObj GetCharacterData()
    {
        return characterData;
    }
}
