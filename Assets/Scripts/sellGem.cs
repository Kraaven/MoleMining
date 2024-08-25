using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sellGem : MonoBehaviour
{
    private int bank = 0;
    private int priceofGem = 200;
    public GameObject popUp;
    public TextMeshProUGUI currentCash;
    private Dictionary<string, int> gemPrices;

    private void Start()
    {
        InitializeGemPrices(); // Ensure this is called to initialize the dictionary
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            sell();
        }
    }

    private void sell()
    {
        priceofGem = gemPrice("Diamond", "Brilliant");
        popUp.SetActive(true);
    }

    public void sellYes()
    {
        bank += priceofGem;
        currentCash.text = $"cash : {bank}";
        popUp.SetActive(false);
    }

    public void sellNo()
    {
        popUp.SetActive(false);
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
            Debug.Log($"{gemName} ({cut}) price not found.");
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
            { "Peridot_Teardrop", 380 },
            { "Peridot_Round", 400 },
            { "Peridot_Pearl", 390 },

            // Garnet
            { "Garnet_Round", 390 },
            { "Garnet_Pearl", 380 },
            { "Garnet_Brilliant", 290 },
            { "Garnet_Princess", 340 },
            { "Garnet_Octagon", 300 },
            { "Garnet_Heart", 320 },
            { "Garnet_Teardrop", 400 },

            // Amber
            { "Amber_Brilliant", 230 },
            { "Amber_Princess", 220 },
            { "Amber_Octagon", 270 },
            { "Amber_Heart", 260 },
            { "Amber_Teardrop", 350 },
            { "Amber_Round", 300 },
            { "Amber_Pearl", 320 },

            // Blue Sapphire
            { "Blue Sapphire_Heart", 660 },
            { "Blue Sapphire_Teardrop", 680 },
            { "Blue Sapphire_Round", 650 },
            { "Blue Sapphire_Pearl", 700 },
            { "Blue Sapphire_Brilliant", 590 },
            { "Blue Sapphire_Princess", 630 },
            { "Blue Sapphire_Octagon", 640 },

            // Pink Tourmaline
            { "Pink Tourmaline_Brilliant", 550 },
            { "Pink Tourmaline_Princess", 680 },
            { "Pink Tourmaline_Octagon", 780 },
            { "Pink Tourmaline_Heart", 1000 },
            { "Pink Tourmaline_Teardrop", 500 },
            { "Pink Tourmaline_Round", 700 },
            { "Pink Tourmaline_Pearl", 650 },

            // Amethyst
            { "Amethyst_Brilliant", 300 },
            { "Amethyst_Princess", 200 },
            { "Amethyst_Octagon", 250 },
            { "Amethyst_Heart", 280 },
            { "Amethyst_Teardrop", 270 },
            { "Amethyst_Round", 300 },
            { "Amethyst_Pearl", 220 },

            // Diamond
            { "Diamond_Brilliant", 900 },
            { "Diamond_Princess", 850 },
            { "Diamond_Octagon", 750 },
            { "Diamond_Heart", 700 },
            { "Diamond_Teardrop", 500 },
            { "Diamond_Round", 800 },
            { "Diamond_Pearl", 690 }
        };
    }
}
