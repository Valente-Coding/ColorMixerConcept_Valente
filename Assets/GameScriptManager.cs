using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class GameScriptManager : MonoBehaviour
{
    // Getting the Blender Top Part
    [SerializeField] private GameObject BlenderTopPart;

    // Getting the Fruits & Vegetables Spawners Scripts
    [SerializeField] private PoolObjectSpawner BananaSpawner;
    [SerializeField] private Color BananaColor;
    [SerializeField] private PoolObjectSpawner AppleSpawner;
    [SerializeField] private Color AppleColor;
    [SerializeField] private PoolObjectSpawner OrangesSpawner;
    [SerializeField] private Color OrangesColor;
    [SerializeField] private PoolObjectSpawner CherriesSpawner;
    [SerializeField] private Color CherriesColor;
    [SerializeField] private PoolObjectSpawner TomatosSpawner;
    [SerializeField] private Color TomatosColor;
    [SerializeField] private PoolObjectSpawner BroccoliSpawner;
    [SerializeField] private Color BroccoliColor;
    [SerializeField] private PoolObjectSpawner EggPlantSpawner;
    [SerializeField] private Color EggPlantColor;

    // Blender Liquid Gameobject
    [SerializeField] private GameObject liquidGO;
    private Renderer liquidMat;

    // List Of Levels From Various ScriptableObjects
    public List<LevelContent> Levels = new List<LevelContent>();

    // Current Level Contents
    private int currentLevel = 0;
    private Color currentObjectiveColor = new Color(0f, 0f, 0f);
    private List<Color> colorsInBlender = new List<Color>();
    private Color currentColor = new Color(0f,0f,0f);
    private bool canInsertMore = false;

    //Finish Level
    private bool lockSpawners = false;

    // Result Color UI
    [SerializeField] private Image ResultColorUi;
    [SerializeField] private TextMeshProUGUI ResultColorPercentageText;
    private float ResultColorPercentage;


    private void Start()
    {
        liquidMat = liquidGO.GetComponent<Renderer>();
    }

    public void StartFirstLevel()
    {
        if (Levels.Count > 0)
            LoadLevel(Levels[currentLevel]);
    }

    private void LoadLevel(LevelContent thisLevel)
    {
        UnloadCurrentLevel();
        OpenBlenderTopPart();

        int i = 0;

        for (int b = 0; b < thisLevel.AmountOfBananas; b++)
        {
            currentObjectiveColor += BananaColor;
            i++;
        }

        for (int b = 0; b < thisLevel.AmountOfApples; b++)
        {
            currentObjectiveColor += AppleColor;
            i++;
        }

        for (int b = 0; b < thisLevel.AmountOfCherries; b++)
        {
            currentObjectiveColor += CherriesColor;
            i++;
        }

        for (int b = 0; b < thisLevel.AmountOfOranges; b++)
        {
            currentObjectiveColor += OrangesColor;
            i++;
        }

        for (int b = 0; b < thisLevel.AmountOfTomatos; b++)
        {
            currentObjectiveColor += TomatosColor;
            i++;
        }

        for (int b = 0; b < thisLevel.AmountOfBroccoli; b++)
        {
            currentObjectiveColor += BroccoliColor;
            i++;
        }

        for (int b = 0; b < thisLevel.AmountOfEggPlant; b++)
        {
            currentObjectiveColor += EggPlantColor;
            i++;
        }

        currentObjectiveColor = currentObjectiveColor / i;

        ResultColorUi.color = currentObjectiveColor;

        if (thisLevel.EnableBanana)
            BananaSpawner.SetActiveStatePrefab(true);

        if (thisLevel.EnableApple)
            AppleSpawner.SetActiveStatePrefab(true);

        if (thisLevel.EnableOranges)
            OrangesSpawner.SetActiveStatePrefab(true);

        if (thisLevel.EnableCherries)
            CherriesSpawner.SetActiveStatePrefab(true);

        if (thisLevel.EnableTomatos)
            TomatosSpawner.SetActiveStatePrefab(true);

        if (thisLevel.EnableBroccoli)
            BroccoliSpawner.SetActiveStatePrefab(true);

        if (thisLevel.EnableEggPlant)
            EggPlantSpawner.SetActiveStatePrefab(true);
    }

    private void UnloadCurrentLevel()
    {
        BananaSpawner.SetActiveStatePrefab(false);
        AppleSpawner.SetActiveStatePrefab(false);
        OrangesSpawner.SetActiveStatePrefab(false);
        CherriesSpawner.SetActiveStatePrefab(false);
        TomatosSpawner.SetActiveStatePrefab(false);
        BroccoliSpawner.SetActiveStatePrefab(false);
        EggPlantSpawner.SetActiveStatePrefab(false);

        currentObjectiveColor = new Color(0f, 0f, 0f);
        currentColor = new Color(0f, 0f, 0f);
        liquidMat.material.color = new Color(1f, 1f, 1f);
        colorsInBlender = new List<Color>();
    }



    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (lockSpawners)
                return;

            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (!canInsertMore)
                    return;

                if (raycastHit.collider.CompareTag("Banana"))
                {
                    BananaSpawner.SendObjectToBlender();
                    colorsInBlender.Add(BananaColor);
                }

                if (raycastHit.collider.CompareTag("Apple"))
                {
                    AppleSpawner.SendObjectToBlender();
                    colorsInBlender.Add(AppleColor);
                }

                if (raycastHit.collider.CompareTag("Oranges"))
                {
                    OrangesSpawner.SendObjectToBlender();
                    colorsInBlender.Add(OrangesColor);
                }

                if (raycastHit.collider.CompareTag("Cherries"))
                {
                    CherriesSpawner.SendObjectToBlender();
                    colorsInBlender.Add(CherriesColor);
                }

                if (raycastHit.collider.CompareTag("Tomatos"))
                {
                    TomatosSpawner.SendObjectToBlender();
                    colorsInBlender.Add(TomatosColor);
                }

                if (raycastHit.collider.CompareTag("Broccoli"))
                {
                    BroccoliSpawner.SendObjectToBlender();
                    colorsInBlender.Add(BroccoliColor);
                }

                if (raycastHit.collider.CompareTag("EggPlant"))
                {
                    EggPlantSpawner.SendObjectToBlender();
                    colorsInBlender.Add(EggPlantColor);
                }

                if (raycastHit.collider.CompareTag("Blender"))
                {
                    if (colorsInBlender.Count == 0)
                        return;

                    CloseBlenderTopPart();
                    Invoke("ChangeLiquidColor", 2f);
                }
            }
        }
    }


    private void OpenBlenderTopPart()
    {
        Sequence openAnimation = DOTween.Sequence();
        openAnimation.Append(BlenderTopPart.transform.DOMove(new Vector3(-0.896f, 1.593f, 5.695f), 1f))
            .Insert(0f, BlenderTopPart.transform.DORotate(new Vector3(-10.834f, -169.563f, 45.58f), 0.5f));

        canInsertMore = true;
    }

    private void CloseBlenderTopPart()
    {
        canInsertMore = false;
        Sequence CloseAnimation = DOTween.Sequence();
        CloseAnimation.Append(BlenderTopPart.transform.DOMove(new Vector3(-1.217f, 1.460f, 5.695f), 1f))
            .Insert(0f, BlenderTopPart.transform.DORotate(new Vector3(0f, -165f, 0f), 0.5f));
        CloseAnimation.Append(liquidGO.transform.DOShakeScale(3f, 0.01f));

    }

    public void ChangeLiquidColor()
    {
        colorsInBlender.ForEach((_color) =>
        {
            currentColor += _color;
        });

        currentColor = currentColor / colorsInBlender.Count;

        liquidMat.material.color = currentColor;

        float red = (currentColor.r > currentObjectiveColor.r) ? currentObjectiveColor.r - (currentColor.r - currentObjectiveColor.r) : currentColor.r;
        float green = (currentColor.g > currentObjectiveColor.g) ? currentObjectiveColor.g - (currentColor.g - currentObjectiveColor.g) : currentColor.g;
        float blue = (currentColor.b > currentObjectiveColor.b) ? currentObjectiveColor.b - (currentColor.b - currentObjectiveColor.b) : currentColor.b;
        float RPercentage = (currentObjectiveColor.r != 0) ? (red * 100) / currentObjectiveColor.r : (currentObjectiveColor.r == red) ? 100 : 0;
        float GPercentage = (currentObjectiveColor.g != 0) ? (green * 100) / currentObjectiveColor.g : (currentObjectiveColor.g == green) ? 100 : 0;
        float BPercentage = (currentObjectiveColor.b != 0) ? (blue * 100) / currentObjectiveColor.b : (currentObjectiveColor.b == blue) ? 100 : 0;

        ResultColorPercentage = (float)Math.Truncate((RPercentage + GPercentage + BPercentage) / 3);
        ResultColorPercentageText.text = ResultColorPercentage.ToString() + "%";

        Invoke("CheckColors", 5f);
    }

    public void CheckColors()
    {
        if (ResultColorPercentage >= 90)
        {
            ResultColorPercentageText.text = "0%";
            NextLevel();
        }
        else
        {
            LoadLevel(Levels[currentLevel]);
        }
    }

    public void NextLevel()
    {
        currentLevel++;

        if (currentLevel >= Levels.Count)
            currentLevel = 0;

        LoadLevel(Levels[currentLevel]);
    }

    public void ResetLevel()
    {
        LoadLevel(Levels[currentLevel]);
    }
}
