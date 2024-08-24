    using System;
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

        public SpriteRenderer SR;

        protected virtual void Start()
        {
            gameObject.AddComponent<GravityComponent>(); 
            gameObject.AddComponent<CircleCollider2D>().radius = 0.08f;
            
   

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
            SR = gameObject.AddComponent<SpriteRenderer>();
            
            SR.sprite = ResourceManager.Instance.GetSprite(Category, Type);
            SR.color = Color;
        }

        private void Update()
        {
            if (transform.position.y < -100)
            {
                Destroy(gameObject);
            }
        }
    }
