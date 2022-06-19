using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    /*
    TO DO Borrar esto al finalizar el script
     Pasos a seguir para hacer el sistema de dialogos
     voy a asumir que el script se le coloca al NPC
        1 - El dialogo inicia cuando el player esá cerca del NPC y apreta la tecla indicada por los GD para interactuar (provisionalmente la E)  
        2 - El player dice un dialogo que da inicio a la conversacion, el NPC responde
        3 - Se ramifican las respuestas del player y una opcion de salir de la conversacion, cada opcion que elija el player tiene una respuesta del NPC
        4 - Cuando se hayan activado todas las ramas si el player sale de la conversacion y vuelve a entrar se activa UN dialogo especifico como dando por hecho que ya le dio toda la info
        
    Pasos a seguir para usar el sistema de dialogos
        1- Crear un text de text mesh pro y ubicarlo en donde queremos que aparezcan los dialogos
        2- Ubicar en el nivel el NPC
        3- Colocar este script en el NPC
        4- Asignar el text del paso 1 al script que le pusimos al NPC
        5- 
         */

    [SerializeField]
    public struct dialogueBranch
    {
        public bool activated;
        public List<string> playerDialog;
        public List<string> NPCDialog;
    }
    public TextMeshProUGUI dialogFrame;
    public float textSpeed;
    public string firstPlayerDialogue;
    public string firstNPCDialogue;
    public List<dialogueBranch> NPCDialogBranches;
    bool dialogueStarted;
    bool playerDialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyUp(KeyCode.E))
        {
            startDialogueSystem();
        }
    }

    private void Update()
    {
        if (dialogueStarted == true)
        {
            if (Input.GetKeyUp(KeyCode.End) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {

            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            dialogueStarted = false;
            dialogFrame.gameObject.SetActive(false);
        }
    }
    void startDialogueSystem()
    {
        dialogueStarted = true;
        dialogFrame.gameObject.SetActive(true);
        dialogFrame.text = firstPlayerDialogue;        
    }

    void setDialogue(int branchIndex, int dialogueIndex, bool isPlayerDialogue)
    {
        if(isPlayerDialogue)
        { 
            dialogFrame.text = NPCDialogBranches[branchIndex].playerDialog[dialogueIndex];
        }
        else 
        {
            dialogFrame.text = NPCDialogBranches[branchIndex].NPCDialog[dialogueIndex];
        }
    }

    
}
