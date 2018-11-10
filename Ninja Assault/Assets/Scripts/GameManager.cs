using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private int quantityOfEnemies;

    void ToInstance() {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }
    private void Awake() {
        ToInstance();        
    }
	// Update is called once per frame
	void Update () {
		
	}

    public bool HasEnemyLeft() {
        if (quantityOfEnemies <= 0)
            return false;
        else
            return true;
    }

    public void AddEnemy() {
        quantityOfEnemies++;
        HasEnemyLeft();
    }

    public int GetEnemyQuantity() {
        return quantityOfEnemies;
    }

    public void LessOneEnemy() {
        quantityOfEnemies--;
        HasEnemyLeft();
    }
}
