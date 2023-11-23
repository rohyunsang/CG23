using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSpawnInfo {
    public GameObject monsterPrefab; // Reference to the monster prefab
    public int quantity;             // Quantity of this monster type
    public Transform spawnPoint;     // Spawn point for the monsters

    public MonsterSpawnInfo(GameObject prefab, int qty, Transform spawn) {
        monsterPrefab = prefab;
        quantity = qty;
        spawnPoint = spawn;
    }
}

public class MonsterSpawn : MonoBehaviour
{
    public StageData stageData; // Assign this in the Unity Editor or find it dynamically
    public int currentStage = 1;

    public void StartStage() {  //using btn only debug
        SpawnMonstersForStage(stageData.stageLevel);
        currentStage++;
    }

    public void SpawnMonstersForStage(int stageNumber) {
        List<MonsterSpawnInfo> currentStageMonsters = stageData.GetMonsterSpawnInfoForStage(stageNumber);

        foreach (MonsterSpawnInfo monsterInfo in currentStageMonsters) {
            for (int i = 0; i < monsterInfo.quantity; i++) {
                Instantiate(monsterInfo.monsterPrefab, monsterInfo.spawnPoint.position, Quaternion.identity);
            }
        }
    }
}