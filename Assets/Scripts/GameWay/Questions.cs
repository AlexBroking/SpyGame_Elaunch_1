using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{

    private GameObject questionTab;

    private Vector3 mousePos;
    private bool openTab = false;
    private int jsonQuestion;

    private PlayerInventoryButton invScript;
    private PlayerControllerMovement playerMove;
    private GameObject controlCircle;
    private GameObject dashButton;

    private GameObject thisQuestionDoor;
    private bool doorAnswer;

    public TextAsset jsonFile;
    private Vragen QuestionsInJson;

    private void Start()
    {
        dashButton = GameObject.Find("DashButton");
        invScript = this.gameObject.GetComponent<PlayerInventoryButton>();
        playerMove = this.gameObject.GetComponent<PlayerControllerMovement>();
        controlCircle = GameObject.Find("Control_Circle");
        questionTab = GameObject.Find("QuestionsTab");

        questionTab.gameObject.SetActive(false);


        QuestionsInJson = JsonUtility.FromJson<Vragen>(jsonFile.text);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                mousePos = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    if (openTab == true)
                    {
                        for (int i = 0; i < questionTab.transform.GetChild(0).childCount; i++) 
                        {
                            mousePos = new Vector3(mousePos.x, mousePos.y, questionTab.transform.GetChild(0).GetChild(i).transform.position.z);
                            if (questionTab.transform.GetChild(0).GetChild(i).GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                            {
                                if (questionTab.transform.GetChild(0).GetChild(i).name == "0")
                                {
                                    if (QuestionsInJson.vragen[jsonQuestion].A0[jsonQuestion].Answer.ToString() == "False")
                                    {
                                        Debug.Log("false");
                                        doorAnswer = false;
                                    }

                                    if (QuestionsInJson.vragen[jsonQuestion].A0[jsonQuestion].Answer.ToString() == "true")
                                    {
                                        Debug.Log("true");
                                        doorAnswer = true;
                                    }
                                }

                                if (questionTab.transform.GetChild(0).GetChild(i).name == "1")
                                {
                                    if (QuestionsInJson.vragen[jsonQuestion].A1[jsonQuestion].Answer.ToString() == "False")
                                    {
                                        Debug.Log("false");
                                        doorAnswer = false;
                                    }

                                    if (QuestionsInJson.vragen[jsonQuestion].A1[jsonQuestion].Answer.ToString() == "true")
                                    {
                                        Debug.Log("true");
                                        doorAnswer = true;
                                    }
                                }

                                if (questionTab.transform.GetChild(0).GetChild(i).name == "2")
                                {
                                    if (QuestionsInJson.vragen[jsonQuestion].A2[jsonQuestion].Answer.ToString() == "False")
                                    {
                                        Debug.Log("false");
                                        doorAnswer = false;
                                    }

                                    if (QuestionsInJson.vragen[jsonQuestion].A2[jsonQuestion].Answer.ToString() == "true")
                                    {
                                        Debug.Log("true");
                                        doorAnswer = true;
                                    }
                                }

                                

                                WhatDoorNow(doorAnswer);
                                CloseQuestion();
                            }
                        }

                        mousePos = new Vector3(mousePos.x, mousePos.y, questionTab.transform.GetChild(1).transform.position.z);
                        if (questionTab.transform.GetChild(1).GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                        {
                            CloseQuestion();
                        }

                    }
                }

            }
        }
    }

    public void OpenQuestion(string doorId, GameObject thisDoor)
    {
        thisQuestionDoor = thisDoor;
        openTab = true;
        playerMove.canMove = false;
        invScript.inventoryManager.SetActive(false);
        invScript.ButtonIcon.SetActive(false);
        controlCircle.SetActive(false);
        dashButton.SetActive(false);
        questionTab.SetActive(true);

        for (int i = 0; i < QuestionsInJson.vragen.Length; i++)
        {
            if (QuestionsInJson.vragen[i].id.ToString() == doorId)
            {
                // Proftekst // 
                questionTab.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = QuestionsInJson.vragen[i].ProfTekst.ToString();
                questionTab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = QuestionsInJson.vragen[i].A0[0].Tekst.ToString();
                questionTab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = QuestionsInJson.vragen[i].A1[0].Tekst.ToString();
                questionTab.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = QuestionsInJson.vragen[i].A2[0].Tekst.ToString();
                jsonQuestion = i;
            }
        }
    }

    public void CloseQuestion()
    {
        playerMove.canMove = true;
        invScript.inventoryManager.SetActive(true);
        invScript.ButtonIcon.SetActive(true);
        controlCircle.SetActive(true);
        questionTab.SetActive(false);
        dashButton.SetActive(true);
        openTab = false;
    }

    public void WhatDoorNow(bool Answer)
    {
        if (thisQuestionDoor.GetComponent<NewDoorBehaviour>().Qdoor == true)
        {
            thisQuestionDoor.transform.parent.GetComponent<Animator>().speed = 1;
            thisQuestionDoor.GetComponent<NewDoorBehaviour>().Qdoor = false;
            thisQuestionDoor.GetComponent<NewDoorBehaviour>().greenDoor = true;
        }

        if (thisQuestionDoor.GetComponent<NewDoorBehaviour>().Pdoor == true)
        {
            if (Answer == true)
            {
                thisQuestionDoor.transform.parent.GetComponent<Animator>().speed = 1;
                thisQuestionDoor.GetComponent<NewDoorBehaviour>().Pdoor = false;
                thisQuestionDoor.GetComponent<NewDoorBehaviour>().greenDoor = true;
            }

            if (Answer == false)
            {
                thisQuestionDoor.transform.parent.GetComponent<Animator>().speed = 1;
                thisQuestionDoor.GetComponent<NewDoorBehaviour>().Pdoor = false;
                thisQuestionDoor.GetComponent<NewDoorBehaviour>().blackDoor = true;
            }
        }
    }
}