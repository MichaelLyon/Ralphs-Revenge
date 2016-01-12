using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections;
using GooglePlayGames;
public class Menus : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
    public GameObject armory;
    public GameObject levelSelect;

    public enum Menu
    {
        Main,
        Options,
        Armory,
        LevelSelect
    }

    public Menu menu;


    void Start()
    {

        menu = Menu.Main;
        SetAnimatorEnum(menu);
    }

    public void SlideInOptions()
    {
        menu = Menu.Options;
        SetAnimatorEnum(menu);
    }

    public void SlideInMainMenu()
    {
        menu = Menu.Main;
        SetAnimatorEnum(menu);
    }

    public void SlideInArmory()
    {
        menu = Menu.Armory;
        SetAnimatorEnum(menu);
    }

    public void SlideInLevelSelect()
    {
        menu = Menu.LevelSelect;
        SetAnimatorEnum(menu);
    }

    public void SetAnimatorEnum(Menu menu)
    {
        mainMenu.GetComponent<Animator>().SetInteger("MenuEnum", (int)menu);
        options.GetComponent<Animator>().SetInteger("MenuEnum", (int)menu);
        armory.GetComponent<Animator>().SetInteger("MenuEnum", (int)menu);
        levelSelect.GetComponent<Animator>().SetInteger("MenuEnum", (int)menu);
        AudioManager.Instance.PlayButtonPress();
    }

    public void GotoScene(string sceneName)
    {
        AudioManager.Instance.PlayButtonPress();
        Application.LoadLevel(sceneName);
    }
}
