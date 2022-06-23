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

    [System.Serializable][SerializeField]
    public struct dialogueBranch
    {
        public bool branchDone;
        public List<string> playerDialog;
        public List<string> NPCDialog;
    }

    public string firstPlayerDialogue;
    public string firstNPCDialogue;
    public List<dialogueBranch> NPCDialogBranches;
    public string allDoneNPCDialogue;





    
    void dialogueBranchDone(int index)
    {
        if(NPCDialogBranches.Count> index)
        { 
            //NPCDialogBranches[index].branchDone = true;
        }
    }

    
}
