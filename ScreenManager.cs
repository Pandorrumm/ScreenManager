using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    [SerializeField] private GameObject moneyDisplay;
    [SerializeField] private GameObject plusIcon;
    [SerializeField] private Button moneyDisplayButton;

    public ScreenType currentScreenType;

    [Serializable]
    public class Screen
    {
        public ScreenType screenType;
        public GameObject screenObject;
        public bool isShowMoneyDisplay;
        public bool isPlusButtonActive;
    }

    [Space]
    [SerializeField] private List<Screen> screens = new List<Screen>();

    private void Awake()
    {
        Instance = this;
    }

    public void ShowScreen(ScreenType _closeScreenType, ScreenType _openScreenType)
    {
        AudioManager.PlayDefault(AudioManager.TAP_BUTTON);

        CloseScreen(_closeScreenType);
        OpenScreen(_openScreenType);
    }

    private void CloseScreen(ScreenType _closeScreenType)
    {
        GetScreenObject(_closeScreenType).SetActive(false);
    }

    private void OpenScreen(ScreenType _openScreenType)
    {
        GetScreenObject(_openScreenType).SetActive(true);

        SetStatusMoneyDisplay(_openScreenType);

        currentScreenType = _openScreenType;
    }

    private GameObject GetScreenObject(ScreenType _screenType)
    {
        for (int i = 0; i < screens.Count; i++)
        {
            if (screens[i].screenType == _screenType)
            {
                return screens[i].screenObject;
            }
        }

        return null;
    }

    private void SetStatusMoneyDisplay(ScreenType _screenType)
    {
        if (IsShowMoneyDisplay(_screenType))
        {
            moneyDisplay.SetActive(true);

            if (moneyDisplayButton != null)
            {
                SetStatusMoneyDisplayButton(_screenType);
            }
        }
        else
        {
            moneyDisplay.SetActive(false);
        }
    }

    private bool IsShowMoneyDisplay(ScreenType _screenType)
    {
        for (int i = 0; i < screens.Count; i++)
        {
            if (screens[i].screenType == _screenType)
            {
                return screens[i].isShowMoneyDisplay;
            }
        }

        return true;
    }

    private void SetStatusMoneyDisplayButton(ScreenType _screenType)
    {
        for (int i = 0; i < screens.Count; i++)
        {
            if (screens[i].screenType == _screenType)
            {
                if (!screens[i].isPlusButtonActive)
                {
                    ButtonActivity(false);
                }
                else
                {
                    ButtonActivity(true);
                }
            }
        }
    }

    private void ButtonActivity(bool _enable)
    {
        plusIcon.SetActive(_enable);
        moneyDisplayButton.enabled = _enable;
    }
}

public enum ScreenType
{
    MAIN,
    SETTINGS,
    DAILY,
    PURCHASES,
    LEVEL_SELECT,
    SKINS,
    PRODUCT_SCREEN,
    GAME_SCREEN,
    MENU_SCREEN
}
