using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
     [System.NonSerialized]
     public List<Collider> fruit = new List<Collider>();
 
     private void OnTriggerEnter (Collider other) {
         if (!fruit.Contains(other) && other.tag == "Fruit" && other.name == "spine")
         {
             fruit.Add(other);
             Debug.Log(other);
         }
     }
 
     private void OnTriggerExit (Collider other) {
         if (fruit.Contains(other))
         {
             fruit.Remove(other);
             Debug.Log("removed");
         }
    }
}
