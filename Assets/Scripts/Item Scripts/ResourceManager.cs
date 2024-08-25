using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

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
    [SerializeField] private List<ItemEntry> Crystals;

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
            case ItemCategory.Crystal:
                return Crystals;
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

    private void Start()
    {
        
        // var IT = new GameObject();
        // IT.transform.position = Vector3.zero;
        // IT.transform.localScale *= 10;
        // IT.layer = LayerMask.NameToLayer("Item");
        //
        // var TT = IT.AddComponent<Item>();
        // Material[] values = (Material[])System.Enum.GetValues(typeof(Material));
        // Material randomMaterial = values[Random.Range(0, values.Length)];
        // TT.Initialize("TEST",randomMaterial,ItemCategory.Crystal,"Crystal1");
        
        
        //StartCoroutine(Spawn());
    }


    
    IEnumerator Spawn()
    {
        yield return null;
        yield return null;
        for (int i = 0; i < 50 ;i++)
        {
            var IT = new GameObject();
            IT.transform.Translate(new Vector3(Random.Range(-6f,6f),4,0));
            IT.transform.localScale *= 2;
            IT.layer = LayerMask.NameToLayer("Item");
            string T = "";
            switch (Random.Range(0,7))
            {
                case 0:
                    T = "Octagon";
                    break;
                case 1:
                    T = "Princess";
                    break;
                case 2:
                    T = "Brilliant";
                    break;
                case 3:
                    T = "Pear";
                    break;
                case 4:
                    T = "Teardrop";
                    break;
                case 5:
                    T = "Round";
                    break;
                case 6:
                    T = "Heart";
                    break;
            }
            var TT = IT.AddComponent<Item>();
            
            Material[] values = (Material[])System.Enum.GetValues(typeof(Material));
            Material randomMaterial = values[Random.Range(0, values.Length)];
            TT.Initialize("TEST",randomMaterial,ItemCategory.Crystal,$"Crystal{Random.Range(1,5)}");
            yield return new WaitForSeconds(0.1f);
        }
    }
}