    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Serialization;
    

    public class Item : MonoBehaviour
    {
        public string Name;
        public Color Color;
        public ItemCategory Category;
        public string Type;
        public string Material;

        protected SpriteRenderer SR;

        protected virtual void Start()
        {
            SR = GetComponent<SpriteRenderer>();
            
            print(SR);
            gameObject.AddComponent<GravityComponent>();
            
            Initialize(Name, Color,Category,Material,Type );
        }

        public virtual void Initialize(string name, Color color, ItemCategory category, string material, string type = "")
        {
            Name = name;
            Color = color;
            Category = category;
            Material = material;
            Type = type;

            UpdateAppearance();
        }

        protected virtual void UpdateAppearance()
        {
            SR.sprite = ResourceManager.Instance.GetSprite(Category, Type);
            SR.color = Color;
        }
    }
