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
        public Material Material;

        public SpriteRenderer SR;

        protected virtual void Start()
        {
            //gameObject.AddComponent<GravityComponent>(); 
            gameObject.AddComponent<CircleCollider2D>().radius = 0.08f;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.AddComponent<BoxCollider2D>();



        }

        public virtual void Initialize(string name, Material material, ItemCategory category, string type = "")
        {
            Name = name;
            Color = Materials.GetMaterialColor(material);
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
