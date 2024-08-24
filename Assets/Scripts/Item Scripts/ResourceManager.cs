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

    private void Start()
    {
        // for (int i = -2; i < 3; i++)
        // {
        //     var IT = new GameObject();
        //     IT.transform.Translate(new Vector3(i*2,4,0));
        //     IT.transform.localScale *= 10;
        //     IT.layer = LayerMask.NameToLayer("Item");
        //     string T = "";
        //     switch (Random.Range(0,3))
        //     {
        //         case 0:
        //             T = "REC";
        //             break;
        //         case 1:
        //             T = "ROMB";
        //             break;
        //         case 2:
        //             T = "DIA";
        //             break;
        //     }
        //     var TT = IT.AddComponent<Item>();
        //     TT.Initialize("TEST",new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f)),ItemCategory.Gem,"",T);
        //     
        // }
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        yield return null;
        yield return null;
        for (int i = 0; i < 50 ;i++)
        {
            var IT = new GameObject();
            IT.transform.Translate(new Vector3(Random.Range(-6f,6f),4,0));
            IT.transform.localScale *= 10;
            IT.layer = LayerMask.NameToLayer("Item");
            string T = "";
            switch (Random.Range(0,3))
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
            }
            var TT = IT.AddComponent<Item>();
            
            Material[] values = (Material[])System.Enum.GetValues(typeof(Material));
            Material randomMaterial = values[Random.Range(0, values.Length)];
            TT.Initialize("TEST",randomMaterial,ItemCategory.Gem,T);
            yield return new WaitForSeconds(0.1f);
        }
    }
}