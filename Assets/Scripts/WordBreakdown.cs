using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

//this script handles all word operation
public class WordBreakdown : MonoBehaviour
{
    public Levels currentLevel;
    private Dictionary<int, bool> scoreCheck = new Dictionary<int, bool>();
    private GameManager gameManager;

    [HideInInspector]public string[] separatedPoem;

    [SerializeField] private GameObject wordBlock;

    bool levelImplemented = false;

    private void Start()
    {
        GameManager.Instance.OnStageChanged += GameManager_OnStageChanged;

    }

    private void GameManager_OnStageChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            UnpackPoem(currentLevel.levelPoem);
        }
    }


    public void UnpackPoem(string poem)
    {
        //if (!GameManager.Instance.IsGamePlaying()) return;

        string cleanedString = Regex.Replace(poem, "[^a-zA-Z0-9 ]+", "").ToLower();

        //convert the poem into an array(index is important)
        char[] delimeterChar = { ' ' };
        separatedPoem = cleanedString.Split(delimeterChar);

        InstatiateWordBlocks(separatedPoem);
    }


    private void InstatiateWordBlocks(string[] separatedPoem)
    {
        for (int i = 0; i < separatedPoem.Length; i++)
        {
            wordBlock.GetComponentInChildren<TextMeshProUGUI>().text = separatedPoem[i];

            float maxRange = 21f;
            float minRange = -20f;

            for(int j = 0; j < 10; j++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(minRange, maxRange), 0.2f, Random.Range(minRange, maxRange));

                if (FindCollisions(randomPosition) < 1 || j == 9)
                {
                    Instantiate(wordBlock, randomPosition, wordBlock.transform.rotation);
                    break;
                }
            }
        }
    }

    private int FindCollisions(Vector3 spawnPosition)
    {
        Collider[] hits = Physics.OverlapSphere(spawnPosition, 5f);
        return hits.Length;
    }


    public void GetPlayerScore(string[] wordsFoundByPlayer, string[] separatedPoem)
    {
        for (int i = 0; i < separatedPoem.Length; i++)
        {
            if (wordsFoundByPlayer[i] == separatedPoem[i])
            {
                scoreCheck.Add(i, true);
            }
            else
            {
                scoreCheck.Add(i, false);
            }
        }
        Debug.Log(scoreCheck);
    }

}
