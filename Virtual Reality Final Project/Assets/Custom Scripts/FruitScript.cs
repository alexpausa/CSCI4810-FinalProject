using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public string fruitType;

    public ConfigurableJoint spine;
    public Transform fruitTransform;

    public SkinnedMeshRenderer mesh;
    public Material color;
    public GameObject mainParent;       // To destroy parent when blending the fruit

    private HingeJoint[] hj;
    private JointDrive xDrive;
    private JointDrive yzDrive;
    private bool isDead = false;

    public bool isRagdoll;
    private Vector3 fruitRotation;

    // Start is called before the first frame update
    void Start()
    {
    
        xDrive = spine.angularXDrive;
        yzDrive = spine.angularYZDrive;
    
        hj = GetComponentsInChildren<HingeJoint>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ragdollCheck();

        // move spine.targetRotation every frame if the fruit is in ragdoll state
        // otherwise move the traget rotation every so often to change the direction that
        // the fruit is moving in.

        // Add movement to the fruit and pause for a few seconds.
        if (!isDead) {
            fruitRotation = fruitTransform.rotation.eulerAngles;
            changeDirections();
        }
    }

    private void ragdollCheck()
    {
        if (isRagdoll || isDead)
        {
            JointDrive currentCJoint;
            
            currentCJoint = spine.angularXDrive;
            currentCJoint.positionSpring = 0.0f;
            spine.angularXDrive = currentCJoint;
            spine.angularYZDrive = currentCJoint;
            

            foreach(HingeJoint hinge in hj)
            {
                hinge.useSpring = false;
            }
        }
        else
        {
            spine.angularXDrive = xDrive;
            spine.angularYZDrive = yzDrive;
            
            foreach (HingeJoint hinge in hj)
            {
                hinge.useSpring = true;
            }
        }
    }

    private void changeDirections()
    {
        // add other requirements such as time waited
        if(isRagdoll)
        {
            //Quaternion rotation = new Quaternion();
            //rotation.eulerAngles = new Vector3(0.0f, fruitRotation.y, fruitRotation.z);

            spine.targetRotation = Quaternion.Euler(0.0f, 360.0f, 0.0f);
        }
    }

    public void ragdollEnable()
    {
        isRagdoll = true;
    }

    public void ragdollDelayedDisable()
    {
        Invoke("ragdollDisable", 2);
    }

    public void ragdollDisable()
    {
        isRagdoll = false;
    }

    public void killFruit()
    {
        isDead = true;
        isRagdoll = true;

        Material[] materials = mesh.materials;
        for (int i = 0; i < materials.Length; i++) {
            if (materials[i].name == "Pupils (Instance)" || materials[i].name == "Eyes (Instance)")
            {
                materials[i] = color;
            }
        }

        mesh.materials = materials;

        // Add some animation stuff and possibly particles at somepoint
        // You may have to remake this game at somepoint to make it actually work well.
    }

    public bool IsDead()
    {
        return isDead;
    }
}
