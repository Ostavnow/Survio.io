using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameOdjectArray
{
    public GameObject[] Loots;
}
//Файл где хранится лут
public class RandomLoots : MonoBehaviour
{
    public GameObject pusher;
    public GameOdjectArray[] LootsNotmal;
    public GameOdjectArray[] LootsRare;
    public GameOdjectArray[] LootsEpic;
    public GameOdjectArray[] LootsMythical;
    public GameOdjectArray[] LootsLegendary;
    public GameOdjectArray[] LootsMedications;
    public GameOdjectArray[] LootsBush;


}
