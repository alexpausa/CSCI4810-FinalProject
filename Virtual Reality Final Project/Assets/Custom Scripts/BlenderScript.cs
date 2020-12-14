using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BlenderScript : MonoBehaviour
{
    private Interactable interactable;
    public ContainerScript container;
    public JuiceScript juice;

    private Color bananaYellow = new Color(1.0f, 0.92f, 0.016f, 1.0f);
    private Color stawberryRed = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    private bool blend;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        blend = false;
    }

    public void TurnOnBlender()
    {
        for (int i = 0; i < container.fruit.Count; i++)
        {
            string c = container.fruit[i].GetComponent<FruitScript>().fruitType;
            if (c == "Banana")
            {
                juice.AddJuice(0.2f, bananaYellow);

                Destroy(container.fruit[i].GetComponent<FruitScript>().mainParent);
                Destroy(container.fruit[i].gameObject);
                container.fruit.RemoveAt(i);
                i--;
                blend = true;
            }
            else if (c == "Strawberry")
            {
                juice.AddJuice(0.2f, stawberryRed);

                Destroy(container.fruit[i].GetComponent<FruitScript>().mainParent);
                Destroy(container.fruit[i].gameObject);
                container.fruit.RemoveAt(i);
                i--;
                blend = true;
            }
        }
        if (blend)
        {
            blend = false;
            GetComponent<AudioSource>().Play();
        }
    }
}
