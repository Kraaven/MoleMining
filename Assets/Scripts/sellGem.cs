using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sellGem : MonoBehaviour
{
    private int bank = 0;
    private int priceofGem;
    private int priceofIngot;

    public GameObject popUp;
    public TextMeshProUGUI currentCash;
    private Dictionary<string, int> gemPrices;
    private Dictionary<string, int> ingotPrices;

    private InventoryItem selectedItem; // Store the selected item when starting the sell process

    private void Start()
    {
        InitializeGemPrices(); // Initialize gem prices
        InitializeIngotPrices(); // Initialize ingot prices
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (InventoryItem.HoveredItem != null && InventoryItem.HoveredItem.ItemInformation.Category == ItemCategory.Gem)
            {
                selectedItem = InventoryItem.HoveredItem; // Store the currently hovered item
                string Mat = selectedItem.ItemInformation.ItemMaterial.ToString();
                string Cut = selectedItem.ItemInformation.ObjectType.ToString();
                Debug.Log($"{Mat} GemStone : {Cut}");
                priceofGem = gemPrice(Mat, Cut);
                sell();
            }
        }
    }

    private void sell()
    {
        popUp.SetActive(true);
    }

    public void sellYes()
    {
        if (selectedItem != null && selectedItem.InventorySlot != null)
        {
            bank += priceofGem; // Use priceofIngot or priceofGem as needed
            InventoryController.DeleteItem(selectedItem.InventorySlot);
            currentCash.text = $"Cash: {bank}";
            popUp.SetActive(false);
            selectedItem = null; // Clear the reference after selling
        }
        else
        {
            Debug.LogWarning("SelectedItem or InventorySlot is null. Cannot delete item.");
        }
    }

    public void sellNo()
    {
        popUp.SetActive(false);
        selectedItem = null; // Clear the reference if selling is canceled
    }

    int gemPrice(string gemName, string cut)
    {
        string key = $"{gemName}_{cut}";
        if (gemPrices.TryGetValue(key, out int price))
        {
            return price;
        }
        else
        {
            Debug.LogWarning($"{gemName} ({cut}) price not found.");
            return 0;
        }
    }

    int ingotPrice(string ingotName)
    {
        if (ingotPrices.TryGetValue(ingotName, out int price))
        {
            return price;
        }
        else
        {
            Debug.LogWarning($"{ingotName} price not found.");
            return 0;
        }
    }


    void InitializeGemPrices()
    {
        gemPrices = new Dictionary<string, int>
        {
            // Peridot
            { "Peridot_Brilliant", 300 },
            { "Peridot_Princess", 310 },
            { "Peridot_Octagon", 350 },
            { "Peridot_Heart", 340 },
            { "Peridot_TearDrop", 380 },
            { "Peridot_Round", 400 },
            { "Peridot_Pear", 390 },

            // Garnet
            { "Garnet_Round", 390 },
            { "Garnet_Pear", 380 },
            { "Garnet_Brilliant", 290 },
            { "Garnet_Princess", 340 },
            { "Garnet_Octagon", 300 },
            { "Garnet_Heart", 320 },
            { "Garnet_TearDrop", 400 },

            // Amber
            { "Amber_Brilliant", 230 },
            { "Amber_Princess", 220 },
            { "Amber_Octagon", 270 },
            { "Amber_Heart", 260 },
            { "Amber_TearDrop", 350 },
            { "Amber_Round", 300 },
            { "Amber_Pear", 320 },

            // Blue Sapphire
            { "Blue_Sapphire_Heart", 660 },
            { "Blue_Sapphire_TearDrop", 680 },
            { "Blue_Sapphire_Round", 650 },
            { "Blue_Sapphire_Pear", 700 },
            { "Blue_Sapphire_Brilliant", 590 },
            { "Blue_Sapphire_Princess", 630 },
            { "Blue_Sapphire_Octagon", 640 },

            // Pink Tourmaline
            { "Pink_Tormaline_Brilliant", 550 },
            { "Pink_Tormaline_Princess", 680 },
            { "Pink_Tormaline_Octagon", 780 },
            { "Pink_Tormaline_Heart", 1000 },
            { "Pink_Tormaline_TearDrop", 500 },
            { "Pink_Tormaline_Round", 700 },
            { "Pink_Tormaline_Pear", 650 },

            // Amethyst
            { "Amethyst_Brilliant", 300 },
            { "Amethyst_Princess", 200 },
            { "Amethyst_Octagon", 250 },
            { "Amethyst_Heart", 280 },
            { "Amethyst_TearDrop", 270 },
            { "Amethyst_Round", 300 },
            { "Amethyst_Pear", 220 },

            // Diamond
            { "Diamond_Brilliant", 900 },
            { "Diamond_Princess", 850 },
            { "Diamond_Octagon", 750 },
            { "Diamond_Heart", 700 },
            { "Diamond_Teardrop", 500 },
            { "Diamond_Round", 800 },
            { "Diamond_Pear", 690 }
        };
    }

    void InitializeIngotPrices()
    {
        ingotPrices = new Dictionary<string, int>
        {
            {"Gold", 150},
            {"Silver", 100},
            {"Platinum", 200}
        };
    }
}
