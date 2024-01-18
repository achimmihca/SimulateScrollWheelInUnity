using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class SampleSceneControl : MonoBehaviour
{
    private Vector2 simulateScrollWheelDelta;

    // Debugging UI
    private UIDocument uiDocument;
    private Label simulatedScrollWheelLabel;

    private void Start()
    {
        InitUI();
    }

    private void Update()
    {
        // simulate scroll up with w key and scroll down with s key
        if (Keyboard.current.wKey.isPressed)
        {
            simulateScrollWheelDelta = new Vector2(0, 1f);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            simulateScrollWheelDelta = new Vector2(0, -1f);
        }
        else
        {
            simulateScrollWheelDelta = Vector2.zero;
        }

        UpdateScrollWheelSimulation();
        UpdateUI();
    }

    private void UpdateScrollWheelSimulation()
    {
        Mouse mouse = Mouse.current;
        if (simulateScrollWheelDelta != Vector2.zero)
        {
            using (StateEvent.From(mouse, out InputEventPtr eventPtr))
            {
                mouse.scroll.WriteValueIntoEvent(simulateScrollWheelDelta, eventPtr);
                InputSystem.QueueEvent(eventPtr);
            }
        }
    }

    private void InitUI()
    {
        uiDocument = FindObjectOfType<UIDocument>();
        simulatedScrollWheelLabel = uiDocument.rootVisualElement.Q<Label>("simulatedScrollWheelLabel");
    }

    private void UpdateUI()
    {
        simulatedScrollWheelLabel.text = $"simulated scroll wheel: {simulateScrollWheelDelta}";
    }
}
