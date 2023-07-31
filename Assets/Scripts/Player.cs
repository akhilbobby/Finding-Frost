using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;

public class Player : MonoBehaviour {

    //public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private WordBreakdown wordBreakdown;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private List<string> foundWords = new List<string>();


    private void Awake()
    {
        //gameManager = GetComponent<GameManager>();
    }

    //always listen to events in Start
    private void Start()
    {
        //gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update()
    {
        HandleMovement();

    }

    private void OnTriggerEnter(Collider wordBlock)
    {
        foundWords.Add(wordBlock.gameObject.GetComponentInChildren<TextMeshProUGUI>().text);

        Destroy(wordBlock.gameObject);

        //add the backtrack word feature

        if (foundWords.Count == wordBreakdown.separatedPoem.Length)
        {
            string[] wordsFoundByPlayer = foundWords.ToArray();

            wordBreakdown.GetPlayerScore(wordsFoundByPlayer, wordBreakdown.separatedPoem);
        }
    }


    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.MovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = Time.deltaTime * moveSpeed;

        transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero; 

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}
   
