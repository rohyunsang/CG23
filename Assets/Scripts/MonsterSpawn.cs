using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TextMeshProUGUI currentStageTMP;
    public void StartStage() {  //using btn only debug
        SpawnMonstersForStage(stageData.stageLevel);
        stageData.stageLevel++;
        currentStageTMP.text = stageData.stageLevel.ToString();
    }

    public void SpawnMonstersForStage(int stageNumber) {
        List<MonsterSpawnInfo> currentStageMonsters = stageData.GetMonsterSpawnInfoForStage(stageNumber);
        int totalMonster = 0;
        foreach (MonsterSpawnInfo monsterInfo in currentStageMonsters) {
            for (int i = 0; i < monsterInfo.quantity; i++) {
                Instantiate(monsterInfo.monsterPrefab, monsterInfo.spawnPoint.position, Quaternion.identity);
            }
            totalMonster += monsterInfo.quantity;
        }
        // current Remaining Monster GameManager
        GameManager.Instance.remainingMonstersText.text = totalMonster.ToString(); 
        GameManager.Instance.currentMonster = totalMonster;

    }
}