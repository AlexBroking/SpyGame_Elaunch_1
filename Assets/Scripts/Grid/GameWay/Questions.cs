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


    public TextAsset jsonFile;
    private Vragen QuestionsInJson;
    private ProfTekstWay profTekst;

    private void Start()
    {
        dashButton = GameObject.Find("DashButton");
        invScript = this.gameObject.GetComponent<PlayerInventoryButton>();
        playerMove = this.gameObject.GetComponent<PlayerControllerMovement>();
        profTekst = this.gameObject.GetComponent<ProfTekstWay>();
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
                                    if (QuestionsInJson.vragen[jsonQuestion].A0[0].Answer.ToString() == "false")
                                    {
                                        WhatDoorNow(false);
                                    }

                                    if (QuestionsInJson.vragen[jsonQuestion].A0[0].Answer.ToString() == "true")
                                    {
                                        WhatDoorNow(true);
                                    }
                                }

                                if (questionTab.transform.GetChild(0).GetChild(i).name == "1")
                                {
                                    if (QuestionsInJson.vragen[jsonQuestion].A1[0].Answer.ToString() == "false")
                                    {
                                        WhatDoorNow(false);
                                    }

                                    if (QuestionsInJson.vragen[jsonQuestion].A1[0].Answer.ToString() == "true")
                                    {
                                        WhatDoorNow(true);
                                    }
                                }

                                if (questionTab.transform.GetChild(0).GetChild(i).name == "2")
                                {
                                    if (QuestionsInJson.vragen[jsonQuestion].A2[0].Answer.ToString() == "false")
                                    {
                                        WhatDoorNow(false);
                                    }

                                    if (QuestionsInJson.vragen[jsonQuestion].A2[0].Answer.ToString() == "true")
                                    {
                                        
                                        WhatDoorNow(true);
                                    }
                                }

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
        playerMove.canMove = false;
        GameObject.Find("Player").GetComponent<Animator>().SetInteger("PlayerAnimation", 1);
        invScript.inventoryManager.SetActive(false);
        invScript.ButtonIcon.SetActive(false);
        controlCircle.SetActive(false);
        dashButton.SetActive(false);

        StartCoroutine(WaitForQuestion(doorId));
    }

    private IEnumerator WaitForQuestion(string doorId)
    {
        yield return new WaitForSeconds(1);
        openTab = true;
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
        questionTab.SetActive(false);
        openTab = false;
    }

    public void WhatDoorNow(bool Answer)
    {
        if (thisQuestionDoor.GetComponent<NewDoorBehaviour>().Qdoor == true || thisQuestionDoor.GetComponent<NewDoorBehaviour>().Pdoor == true)
        {
            if (Answer == true)
            {
                thisQuestionDoor.transform.parent.GetComponent<Animator>().speed = 1;
                thisQuestionDoor.GetComponent<NewDoorBehaviour>().Pdoor = false;
                thisQuestionDoor.GetComponent<NewDoorBehaviour>().Qdoor = false;
                thisQuestionDoor.GetComponent<NewDoorBehaviour>().greenDoor = true;

                playerMove.canMove = true;
                invScript.inventoryManager.SetActive(true);
                invScript.ButtonIcon.SetActive(true);
                controlCircle.SetActive(true);
            }
        }

        if (Answer == false)
        {
            profTekst.PutText("WrongAnswer");
        }
    }
}