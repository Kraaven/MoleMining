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

    [System.Serializable]
    private class ItemEntry
    {
        public string type;
        public Sprite sprite;
    }

    [SerializeField] private List<ItemEntry> ores;
    [SerializeField] private List<ItemEntry> metals;
    [SerializeField] private List<ItemEntry> gems;
    [SerializeField] private List<ItemEntry> emptyPieces;

    public Sprite GetSprite(ItemCategory category, string type = "")
    {
        List<ItemEntry> list = GetListForCategory(category);
        return FindSprite(list, type);
    }
    

    private List<ItemEntry> GetListForCategory(ItemCategory category)
    {
        switch (category)
        {
            case ItemCategory.Ore:
                return ores;
            case ItemCategory.Metal:
                return metals;
            case ItemCategory.Gem:
                return gems;
            case ItemCategory.EmptyPiece:
                return emptyPieces;
            default:
                Debug.LogError($"Unknown category: {category}");
                return null;
        }
    }

    private Sprite FindSprite(List<ItemEntry> list, string type)
    {
        var entry = list.Find(e => e.type == type);
        if (entry != null)
            return entry.sprite;
        
        Debug.LogWarning($"Sprite not found for type: {type}");
        return null;
    }

    
}