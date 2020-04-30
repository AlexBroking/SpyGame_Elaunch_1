using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfTekstWay : MonoBehaviour
{
    private GameObject controlCircle;
    private GameObject profTalk;
    private GameObject textPlacement;

    public TextAsset jsonFile;
    private ProfTekst TalkingInJson;
    private GameObject dashButton;

    private PlayerInventoryButton invScript;
    private PlayerControllerMovement playerMove;

    private bool openText = false;
    private Vector3 mousePos;


    void Start()
    {
        profTalk = GameObject.Find("ProfTalk");
        textPlacement = profTalk.transform.GetChild(0).GetChild(0).gameObject;
        playerMove = this.gameObject.GetComponent<PlayerControllerMovement>();

   
        controlCircle = GameObject.Find("Control_Circle");
        dashButton = GameObject.Find("DashButton");

        invScript = this.gameObject.GetComponent<PlayerInventoryButton>();
        TalkingInJson = JsonUtility.FromJson<ProfTekst>(jsonFile.text);
        profTalk.SetActive(false);
    }

    private void Update()
    {
        if (openText == true)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);

                    if (touch.phase == TouchPhase.Began)
                    {
                        mousePos = new Vector3(mousePos.x, mousePos.y, profTalk.transform.position.z);

                        if (profTalk.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                        {
                            RemoveText();
                        }
                    }
                }
            }
        }
    }

    public void PutText(string textId, GameObject objectToDestroy)
    {
        openText = true;
        profTalk.SetActive(true);
        for (int i = 0; i < TalkingInJson.Items.Length; i++)
        {
            if (TalkingInJson.Items[i].id.ToString() == textId)
            {
                textPlacement.GetComponent<Text>().text = TalkingInJson.Items[i].Tekst.ToString();
            }
        }
        playerMove.canMove = false;
        controlCircle.SetActive(false);
        dashButton.SetActive(false);
        Destroy(objectToDestroy);
    }

    public void RemoveText()
    {
        openText = false;
        textPlacement.GetComponent<Text>().text = "";
        profTalk.SetActive(false);
        playerMove.canMove = true;
        controlCircle.SetActive(true);
    }
}
