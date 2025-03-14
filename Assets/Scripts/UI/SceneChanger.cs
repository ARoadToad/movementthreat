using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Update is called once per frame
    public UIDocument uiDocument;

    void Start()
    {
        Button stupidButton = uiDocument.rootVisualElement.Q<Button>("TheButton");
        stupidButton.clicked += StartTransition;
    }

    void StartTransition()
    {
        SceneManager.LoadScene("SampleScene");
    }
    /*
     *         var button = new Button { text = "Click me" };
        button.clicked += StartTransition;
        uiDocument.rootVisualElement.Add(button);

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button stupidButton = root.Q<Button>("TheButton");
        stupidButton.clicked += () => StartTransition();
    */
}
