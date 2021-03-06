﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isGameCursorVisible;
    
    public static GameManager instance = null;
    public static bool isCursorVisible;
    public static bool restoreBees = false;
    public static int currentSceneNumber;
    public static int unlockedLevelNumber;

    private static GameObject mainCamera;
    [ShowOnly] public static int beeCount = 10;
    [ShowOnly] public static bool isInLevel;

    [HideInInspector] public enum Costumes {Default, Gangster, Viking, Sombrero, Party, Pirate, Roman, King}
    [HideInInspector] public static Costumes costume = Costumes.Default;
    public static float isSombreroUnlocked;
    public static float isVikinghatUnlocked;
    public static float isPartyhatUnlocked;
    public static float isPiratehatUnlocked;
    public static float isRomanhatUnlocked;
    public static float isKinghatUnlocked;
    public static float isGangsterhatUnlocked;

    void Awake () {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);

        unlockedLevelNumber = PlayerPrefs.GetInt("unlockedLevelNumber", 0);
        isSombreroUnlocked = PlayerPrefs.GetInt("isSombreroUnlocked", 0);
        isVikinghatUnlocked = PlayerPrefs.GetInt("isVikinghatUnlocked", 0);
        isPartyhatUnlocked = PlayerPrefs.GetInt("isPartyhatUnlocked", 0);
        isPiratehatUnlocked = PlayerPrefs.GetInt("isPiratehatUnlocked", 0);
        isRomanhatUnlocked = PlayerPrefs.GetInt("isRomanhatUnlocked", 0);
        isKinghatUnlocked = PlayerPrefs.GetInt("isKinghatUnlocked", 0);
        isGangsterhatUnlocked = PlayerPrefs.GetInt("isGangsterhatUnlocked", 0);

        switch (costume) {
            case Costumes.Default:
                break;
            case Costumes.Sombrero:
                if (isSombreroUnlocked == 0) {
                    costume = Costumes.Default;
                }
                break;
            case Costumes.Party:
                if (isPartyhatUnlocked == 0) {
                    costume = Costumes.Default;
                }
                break;
            case Costumes.Viking:
                if (isVikinghatUnlocked == 0) {
                    costume = Costumes.Default;
                }
                break;
            case Costumes.Pirate:
                if (isPiratehatUnlocked == 0) {
                    costume = Costumes.Default;
                }
                break;
            case Costumes.Roman:
                if (isRomanhatUnlocked == 0) {
                    costume = Costumes.Default;
                }
                break;
            case Costumes.King:
                if (isKinghatUnlocked == 0) {
                    costume = Costumes.Default;
                }
                break;
            case Costumes.Gangster:
                if (isGangsterhatUnlocked == 0) {
                    costume = Costumes.Default;
                }
                break;
            default:
                break;
        }
    }

    private void Start() {
        isCursorVisible = isGameCursorVisible;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = GameObject.Find("Main Camera");
        getAllBeesOnScreen();
    }

    void Update () {
        if (getAllBeesOnScreen() <= 0 && isInLevel) {
            Exit();
            restoreBees = true;
            isInLevel = false;
        }
	}

    void Exit() {
        SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Single);
        beeCount = 10;
        Cursor.visible = true;
    }

    static public void ToggleCursorVisibility(bool isVisible) {
        isCursorVisible = isVisible;
        Cursor.visible = isCursorVisible;
    }

    static int getAllBeesOnScreen() {
        mainCamera = GameObject.Find("Main Camera");
        GameObject[] bees = GameObject.FindGameObjectsWithTag("AIBee");
        GameObject[] mainbees = GameObject.FindGameObjectsWithTag("MainBee");
        int AIBeeCount = 0;
        foreach (GameObject bee in bees) {
            if (bee.transform.position.x >= mainCamera.GetComponent<MainCamera>().offset - 10) {
                AIBeeCount++;
            }
        }
        beeCount = AIBeeCount + mainbees.Length;
        return AIBeeCount + mainbees.Length;
    }

    public static void NextCostume() {
        costume++;
        if ((int)costume >= System.Enum.GetNames(typeof(Costumes)).Length) {
            costume = 0;
        }
    }

    public static void PreviousCostume() {
        costume--;
        if ((int)costume < 0) {
            costume = (Costumes)System.Enum.GetNames(typeof(Costumes)).Length - 1;
        }
    }
}
