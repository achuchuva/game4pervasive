using UnityEngine;
using UnityEngine.InputSystem;
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

    // buttons
    public Button inventoryButton;
    public Button characterButton;
    public Button questButton;


    void Start()
    {
        HideMainMenu();
        SetUpOnClickListeners();
    }

    void Update()
    {

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
                break;
            case MenuType.Characters:
                characterMenu.SetActive(true);
                break;
            case MenuType.Quests:
                questMenu.SetActive(true);
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
