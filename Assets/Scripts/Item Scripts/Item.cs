    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Serialization;
    

    public abstract class Item : MonoBehaviour
    {
        public ItemType Type { get; protected set; }
        public string Name { get; protected set; }
        public Color Color { get; protected set; }
        protected SpriteRenderer SR;

        protected virtual void Awake()
        {
            SR = GetComponent<SpriteRenderer>();
            gameObject.AddComponent<GravityComponent>();
        }

        public abstract void Initialize(string name, Color color);
    }
