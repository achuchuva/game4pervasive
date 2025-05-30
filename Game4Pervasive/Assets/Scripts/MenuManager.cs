using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // enum
    public enum MenuType
    {
        Inventory,
        Characters,
        Quests
    }


    // hide show menu
    // select the menu (inventory, characters, quests)

    // menu object
    public GameObject menu;

    // submenu object
    public GameObject inventoryMenu;
    public GameObject characterMenu;
    public GameObject questMenu;

    // nav parent
    public GameObject navParent;

    // buttons
    public Button inventoryButton;
    public Button characterButton;
    public Button questButton;

    // last selected button
    private Button lastSelectedButton;

    // quest manager
    public QuestManager questManager;

    // inventory manager
    public InventoryManager inventoryManager;


    void Start()
    {
        HideMainMenu();
        SetUpOnClickListeners();
    }

    void Update()
    {
        var current = EventSystem.current.currentSelectedGameObject;

        if (current == null)
        {
            // if no button is selected, return
            return;
        }

        Button button = current.GetComponent<Button>();

        if (current != null && button != null && button != lastSelectedButton)
        {

            button.onClick.Invoke();
            lastSelectedButton = button;
        }
    }

    void SetUpOnClickListeners()
    {
        // set up on click listeners for buttons
        inventoryButton.onClick.AddListener(OnInventoryButtonClicked);
        characterButton.onClick.AddListener(OnCharacterButtonClicked);
        questButton.onClick.AddListener(OnQuestButtonClicked);
    }

    void ShowSubMenu(MenuType menuType)
    {
        // hide all menus
        inventoryMenu.SetActive(false);
        characterMenu.SetActive(false);
        questMenu.SetActive(false);

        // show the selected menu
        switch (menuType)
        {
            case MenuType.Inventory:
                inventoryMenu.SetActive(true);
                inventoryManager.ShowInventory();
                break;
            case MenuType.Characters:
                characterMenu.SetActive(true);
                break;
            case MenuType.Quests:
                questMenu.SetActive(true);
                questManager.ShowQuestBook();
                break;
        }
    }

    public void HideMainMenu()
    {
        // hide main menu
        menu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        // show main menu
        menu.SetActive(true);

        // show inventory menu by default
        ShowSubMenu(MenuType.Inventory);

        // set initial selected button for UI navigation
        EventSystem.current.SetSelectedGameObject(inventoryButton.gameObject);
    }

    // sub menu functions for on clicks
    public void OnInventoryButtonClicked()
    {
        ShowSubMenu(MenuType.Inventory);
    }
    public void OnCharacterButtonClicked()
    {
        ShowSubMenu(MenuType.Characters);
    }

    public void OnQuestButtonClicked()
    {
        ShowSubMenu(MenuType.Quests);
    }
}
