    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Serialization;

    public abstract class Item : MonoBehaviour
    {
        // public ItemType itemType;
        // public Color color;
        // public string ItemName;
        protected SpriteRenderer SR;
        void Start()
        {
            SR = GetComponent<SpriteRenderer>();
            gameObject.AddComponent<GravityComponent>();
            // color = ResourceManager.ItemManager.Colors[Random.Range(0,ResourceManager.ItemManager.Colors.Count)];
            // GetComponent<SpriteRenderer>().sprite = ResourceManager.ItemManager.GetData(itemType,ItemName);
            // GetComponent<SpriteRenderer>().color = color;
        }

        public virtual void INIT()
        {
            
        }
    }
