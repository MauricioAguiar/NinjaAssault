using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManage : MonoBehaviour {

    public List<GameObject> ItemsPool;

    public int commonChance, uncommonChance, rareChance, mythicChance, specialChance;

    private List<GameObject> CommonItems, UncommonItems, RareItems, MythicItems, SpecialItems;

    private bool hasCommon, hasUncommon, hasRare, hasMythic, hasSpecial;

    ItemBehaviour SelectedItem;
    
    // Use this for initialization
	void Start () {
        commonChance = 60;
        uncommonChance = 0;
        rareChance = 20;
        mythicChance = 10;
        specialChance = 0;
        CommonItems = new List<GameObject>();
        UncommonItems = new List<GameObject>();
        RareItems = new List<GameObject>();
        MythicItems = new List<GameObject>();
        SpecialItems = new List<GameObject>();

        Instantiate(SelectPowerUp(), transform.position, Quaternion.identity);
	}

    GameObject SelectPowerUp() {

        for (int i = 0; i < ItemsPool.Count; i++) {
            if (ItemsPool[i].GetComponent<ItemBehaviour>().rarity == ItemBehaviour.Rarity.Common) {
                hasCommon = true;
                CommonItems.Add(ItemsPool[i]);
            }
            if (ItemsPool[i].GetComponent<ItemBehaviour>().rarity == ItemBehaviour.Rarity.Uncommon) {
                UncommonItems.Add(ItemsPool[i]);
                hasUncommon = true;
            }
            if (ItemsPool[i].GetComponent<ItemBehaviour>().rarity == ItemBehaviour.Rarity.Rare) {
                RareItems.Add(ItemsPool[i]);
                hasRare = true;
            }
            if (ItemsPool[i].GetComponent<ItemBehaviour>().rarity == ItemBehaviour.Rarity.Mythic) {
                MythicItems.Add(ItemsPool[i]);
                hasMythic = true;
            }
            if (ItemsPool[i].GetComponent<ItemBehaviour>().rarity == ItemBehaviour.Rarity.Special) {
                SpecialItems.Add(ItemsPool[i]);
                hasSpecial = true;
            }
        }

        ItemBehaviour.Rarity itemChoose = ChanceRandom();

        Debug.Log("A Raridade do drop é: " + itemChoose);

        if (itemChoose == ItemBehaviour.Rarity.Common)
        return CommonItems[Random.Range(0, CommonItems.Count)];
        else if (itemChoose == ItemBehaviour.Rarity.Uncommon)
            return UncommonItems[Random.Range(0, UncommonItems.Count)];
        else if (itemChoose == ItemBehaviour.Rarity.Rare)
            return RareItems[Random.Range(0, RareItems.Count)];
        else if (itemChoose == ItemBehaviour.Rarity.Mythic)
            return MythicItems[Random.Range(0, MythicItems.Count)];
        else if(itemChoose == ItemBehaviour.Rarity.Special)
            return SpecialItems[Random.Range(0, SpecialItems.Count)];

        return null;
    }

        ItemBehaviour.Rarity ChanceRandom() {

        if (hasCommon == false)
            commonChance = 0;
        if (hasUncommon == false)
            uncommonChance = 0;
        if (hasRare == false)
            rareChance = 0;
        if (hasMythic == false)
            mythicChance = 0;
        if (hasSpecial == false)
            specialChance = 0;

        int x = commonChance + uncommonChance + rareChance + mythicChance + specialChance;

        int sorted = Random.Range(0, x);

        Debug.Log(sorted);

        if (sorted <= commonChance)
            return ItemBehaviour.Rarity.Common;
        else if (sorted <= commonChance + uncommonChance)
            return ItemBehaviour.Rarity.Uncommon;
        else if (sorted <= commonChance + uncommonChance + rareChance)
            return ItemBehaviour.Rarity.Rare;
        else if (sorted <= commonChance + uncommonChance + rareChance + mythicChance)
            return ItemBehaviour.Rarity.Mythic;
        else if (sorted <= commonChance + uncommonChance + rareChance + mythicChance + specialChance)
            return ItemBehaviour.Rarity.Special;

        return ItemBehaviour.Rarity.Common; 
    }
}
