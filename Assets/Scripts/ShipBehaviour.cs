using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 25f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float cooldownTime = 3f;
    [SerializeField] private Image itemImageHolder;
    [SerializeField] private TMP_Text introductionField;
    [SerializeField] private TMP_Text messageField;

    private float cooldownCounter = 0f;
    private List<Color> items = new List<Color>();
    private int activeItemIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Introduction());
    }
    IEnumerator Introduction() { 
        introductionField.enabled = true;
        introductionField.text = "Welcome to Space 4 8. \n Move your ship with the arrows or WASD. \n Shoot with SPACE. \n Gather pickups and cycle with 'Left CTR'.  \n  Use pickups with 'E'.";
        yield return new WaitForSeconds(5f);
        introductionField.enabled = false;
    }
    IEnumerator ShowMessage(string message) {
        messageField.enabled = true;
        messageField.text = message;
        yield return new WaitForSeconds(3f);
        messageField.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        Move();   
        Rotate();
        Shoot();
        CycleItems();
        UseItem();

    }

    void Move() {

        transform.position = transform.position + transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        
    }
    void Rotate()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }
    void Shoot() { 
        cooldownCounter += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && cooldownCounter > cooldownTime)
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.transform.position = transform.position;
            laser.transform.rotation = transform.rotation;
            Destroy(laser, 3f);

            cooldownCounter = 0f;

        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item")) {
            PickUpItem(other.gameObject);
        }
    }
    void PickUpItem(GameObject item) {

        Color color = item.gameObject.GetComponent<Renderer>().material.color;

        Destroy(item);

        items.Add(color);

        activeItemIndex = items.Count - 1;

        itemImageHolder.color = items[activeItemIndex];
        itemImageHolder.enabled = true;
    }
    
    void CycleItems() {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (items.Count > 0)
            {
                if (activeItemIndex < items.Count - 1)
                {
                    activeItemIndex++;
                }
                else
                {
                    activeItemIndex = 0;
                }
                itemImageHolder.color = items[activeItemIndex];
            }
            else
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }
        }        
    }
    void UseItem()
    {
  
        if (Input.GetKeyDown(KeyCode.E) && items.Count > 0 && activeItemIndex != -1) {

            if (items[activeItemIndex] == Color.blue) {
                StartCoroutine(ShowMessage(" +  Move Speed"));
                moveSpeed += 5;
            }
            else if (items[activeItemIndex] == Color.red){
                StartCoroutine(ShowMessage(" + Fire Rate"));
                cooldownTime -= 0.1f;
            }
            else if(items[activeItemIndex] == Color.green){
                StartCoroutine(ShowMessage(" + Rotation Speed"));
                rotationSpeed += 10;
            }      
            items.RemoveAt(activeItemIndex);            
            if (activeItemIndex > 0)
            {
                activeItemIndex--;
                itemImageHolder.color = items[activeItemIndex];
            }
            else if(items.Count == 0)
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }
            
        }
    }

    /*TO DO 
    
    Optie 1:

    void GetHit(){ 
        //zorg voor enemies die terugschieten. Als je geraakt wordt gaan er levens af. als je levens op zijn ben je af en herstart de game.
    }  
    void HealthBoost(){ 
        //zorg voor een extra powerup die je een health boost geeft
    }

    Optie 2:

    void ActivateShield(){ 
        //Zorg voor een energie schild dat aangezet kan worden     
    }
    void DeactivateShield(){
        //Zorg dat je het schild uit kunt zetten om energie te sparen
    }
    void CheckShieldEnergy(){
        //zorg dat je energie op gaat bij gebruik van het schild
        //is de energie op dan gaat het schild uit
    }
    void RegenerateShield(){
        //Zorg dat je schild langzaam regenereert
    } 

    */

}
