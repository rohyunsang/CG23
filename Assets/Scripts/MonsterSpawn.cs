using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public int currentStage = 0;
    public 
    void CheckLevel(){

    }
    void CheckStatus(int currentStage){

    }
    void SetMonsterStatus(){

    }

    public void SpawnMonster(){
        CheckLevel();
        CheckStatus(-1);
        SetMonsterStatus();
    }

    /*
    public GameObject[] monsterPrefabStageOne; // as prefab

    public void MonsterSpawnStageOne(){
        SpawnMonster(monsterPrefabStageOne, 20);
    }

    public void SpawnMonster(GameObject[] monsterPrefab, int maxMonsters)
    {
        if (monsterPrefab.Length == 0) return; // 프리팹 배열이 비어있으면 반환

        for(int i = 0; i < maxMonsters; i++)
        {
            // monsterPrefab 배열에서 무작위로 몬스터 프리팹을 선택
            GameObject selectedMonsterPrefab = monsterPrefab[Random.Range(0, monsterPrefab.Length)];

            // GetRandomPosition 메소드를 사용하여 랜덤한 위치를 얻습니다.
            Vector3 spawnPosition = GetRandomPosition();

            // 랜덤한 위치에 몬스터 인스턴스를 생성
            Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(0, 2) == 0 ? Random.Range(-50, -40) : Random.Range(40, 50);
        float z = Random.Range(0, 2) == 0 ? Random.Range(-50, -40) : Random.Range(40, 50);
        float y = 5f;
        return new Vector3(x, y, z);
    }
    */
    
}