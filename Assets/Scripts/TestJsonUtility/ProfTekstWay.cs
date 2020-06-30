using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfTekstWay : MonoBehaviour
{
    
    private GameObject profTalk;
    private GameObject textPlacement;

    public TextAsset jsonFile;
    private ProfTekst TalkingInJson;

    private GameObject controlCircle;
    private GameObject dashButton;
    private GameObject invButton;

    private PlayerInventoryButton invScript;
    private PlayerControllerMovement playerMove;

    public bool openText = false;
    private Vector3 mousePos;
    public Sprite Max;
    public Sprite James;

    private int countNumber;
    private int countText;
    private int countStart = 0;
    private bool placingText = false;
    private string str;
    private string strComplete;
    private float tekstSpeed = 0.01f;
    private bool clickedOnTyping = false;

    void Start()
    {
        profTalk = GameObject.Find("ProfTalk");
        textPlacement = profTalk.transform.GetChild(0).GetChild(0).gameObject;
        playerMove = this.gameObject.GetComponent<PlayerControllerMovement>();

   
        controlCircle = GameObject.Find("Control_Circle");
        dashButton = GameObject.Find("DashButton");
        invButton = GameObject.Find("InventarisButton");

        invScript = this.gameObject.GetComponent<PlayerInventoryButton>();
        TalkingInJson = JsonUtility.FromJson<ProfTekst>(jsonFile.text);
        profTalk.SetActive(false);
    }

    private void Update()
    {
        if (openText == false)
        {
            countStart = 0;
            countText = 0;
        }


        if (openText == true)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    mousePos = new Vector3(mousePos.x, mousePos.y, profTalk.transform.position.z);

                    if (touch.phase == TouchPhase.Began)
                    {
                        if (placingText == false)
                        {
                            if (profTalk.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                            {
                                if (countStart == countText)
                                {
                                    RemoveText();
                                    str = "";
                                }

                                if (countStart != countText)
                                {
                                    placingText = true;
                                    clickedOnTyping = true;
                                    countStart++;
                                    str = "";
                                    StartCoroutine(setTextLetter());
                                }
                            }
                        }
                        
                        if (placingText == true)
                        {
                            if (clickedOnTyping == false)
                            {
                                if (profTalk.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                                {
                                    clickedOnTyping = true;
                                    str = "";
                                    textPlacement.GetComponent<Text>().text = strComplete;
                                    placingText = false;
                                }
                            } else
                            {
                                clickedOnTyping = false;
                            }
                        }
                        
                    }
                }
            }
        }


        
    }

    public void PutText(string textId)
    {
        if (openText == false)
        {
            if (textId != "")
            {

                openText = true;
                profTalk.SetActive(true);
                playerMove.canMove = false;
                GameObject.Find("Player").GetComponent<Animator>().SetInteger("PlayerAnimation", 1);
                controlCircle.SetActive(false);
                dashButton.SetActive(false);
                invButton.SetActive(false);

                LetterText(textId);
            }
            else
            {
                RemoveText();
            }
        }
    }

    public void LetterText(string textId)
    {
        for (int j = 0; j < TalkingInJson.Items.Length; j++)
        {
            if (TalkingInJson.Items[j].id.ToString() == textId)
            {
                countNumber = j;
                countText = TalkingInJson.Items[countNumber].TekstArray.Length - 1;
            }
        }
        
        placingText = true;
        StartCoroutine(setTextLetter());
        
    }

    public void RemoveText()
    {
        Debug.Log("test");
        openText = false;
        textPlacement.GetComponent<Text>().text = "";
        profTalk.SetActive(false);
        playerMove.canMove = true;
        controlCircle.SetActive(true);
        invButton.SetActive(true);
    }

    public IEnumerator setTextLetter()
    {
        textPlacement.GetComponent<Text>().text = "";
        if (placingText == true)
        {
            
            strComplete = TalkingInJson.Items[countNumber].TekstArray[countStart].tekst.ToString();

            if (TalkingInJson.Items[countNumber].TekstArray[countStart].Name.ToString() == "Max")
            {
                profTalk.GetComponent<SpriteRenderer>().sprite = Max;
            }

            if (TalkingInJson.Items[countNumber].TekstArray[countStart].Name.ToString() == "James")
            {
                profTalk.GetComponent<SpriteRenderer>().sprite = James;
            }

            if (strComplete == TalkingInJson.Items[countNumber].TekstArray[countStart].tekst.ToString())
            {
                for (int i = 0; i < strComplete.Length; i++)
                {
                    if (placingText == true)
                    {
                        str += strComplete[i];
                        textPlacement.GetComponent<Text>().text = str;
                        yield return new WaitForSeconds(tekstSpeed);

                        if (i == strComplete.Length - 1)
                        {
                            placingText = false;
                            clickedOnTyping = true;
                        }
                    }
                }
            }
        }
    }
}
