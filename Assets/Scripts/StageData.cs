using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageData : MonoBehaviour {
    public int stageLevel = 1;
    public GameObject orcPrefab;  
    public GameObject skeletonPrefab;

    public Transform[] spawnPoints; // Assign these in the Unity Editor

    public List<MonsterSpawnInfo> stage1Monsters;
    public List<MonsterSpawnInfo> stage2Monsters;
    public List<MonsterSpawnInfo> stage3Monsters;

    void Awake() {     
         SetupStages();
    }

    private void SetupStages() {
        // Randomly select the number of spawn points (between 1 and 3)
        int numberOfSpawnPoints = Random.Range(1, 4); // Random.Range is inclusive for min, exclusive for max

        // Example setup for each stage
        for (int i = 0; i < numberOfSpawnPoints; i++) {
            int spawnIndex = Random.Range(0, spawnPoints.Length); // Select a random spawn point index
            stage1Monsters.Add(new MonsterSpawnInfo(orcPrefab, 10, spawnPoints[spawnIndex])); // 10 orcs at a random gate
        }
        // Repeat the process for other stages with different monster setups if needed
    }

    public List<MonsterSpawnInfo> GetMonsterSpawnInfoForStage(int stageNumber) {
        switch (stageNumber) {
            case 1: return stage1Monsters;
            case 2: return stage2Monsters;
            case 3: return stage3Monsters;
            default: return new List<MonsterSpawnInfo>(); // Return empty list for invalid stage
        }
    }
}