using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private List<ItemEntry> crystalVariants;
    [SerializeField] private List<ItemEntry> gemCuts;
    [SerializeField] private List<ItemEntry> metals;
    [SerializeField] private List<ItemEntry> rings;
    [SerializeField] private List<ItemEntry> necklaces;

    public Sprite GetCrystalSprite(string name) => GetSprite(crystalVariants, name);
    public Sprite GetGemSprite(string name) => GetSprite(gemCuts, name);
    public Sprite GetMetalSprite(string name) => GetSprite(metals, name);
    public Sprite GetRingSprite(string name) => GetSprite(rings, name);
    public Sprite GetNecklaceSprite(string name) => GetSprite(necklaces, name);

    private Sprite GetSprite(List<ItemEntry> list, string name)
    {
        var entry = list.Find(e => e.name == name);
        if (entry != null)
            return entry.ItemSprite;
        
        Debug.LogWarning($"Sprite not found: {name}");
        return null;
    }

    public List<Item> Items;
}