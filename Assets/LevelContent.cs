using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContent", menuName = "ScriptableObjects/LevelContent", order = 1)]
public class LevelContent : ScriptableObject
{
    [Header("Bananas")]
    public bool EnableBanana;
    public int AmountOfBananas;

    [Header("Apples")]
    public bool EnableApple;
    public int AmountOfApples;

    [Header("Oranges")]
    public bool EnableOranges;
    public int AmountOfOranges;


    [Header("Cherries")]
    public bool EnableCherries;
    public int AmountOfCherries;

    [Header("Tomatos")]
    public bool EnableTomatos;
    public int AmountOfTomatos;

    [Header("Broccoli")]
    public bool EnableBroccoli;
    public int AmountOfBroccoli;

    [Header("EggPlant")]
    public bool EnableEggPlant;
    public int AmountOfEggPlant;
}
