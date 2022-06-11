using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputActionDisplay : MonoBehaviour
{
    [SerializeField] private InputActionReference actionReference;
    [SerializeField] private int bindingIndex;
    public TextMeshProUGUI testmesh;
    public InputActionDisplay instance;
    private InputAction action;

    private Button rebindButton;

    public void Awake()
    {
        instance = this;
        action = actionReference.action;
        rebindButton = GetComponentInChildren<Button>();
        rebindButton.onClick.AddListener(RebindAction);
    }

    public void OnEnable()
    {
        //action = ControlsRemapping.Controls.asset.FindAction(actionReference.action.id);

        //SetButtonText();
    }

    public void SetButtonText()
    {
        rebindButton.GetComponentInChildren<TextMeshProUGUI>().text = action.GetBindingDisplayString(bindingIndex, InputBinding.DisplayStringOptions.DontUseShortDisplayNames);
    }

    public void RebindAction()
    {
        testmesh.text = "...";

        ControlsRemapping.SuccessfulRebinding += OnSuccessfulRebinding;

        //bool isGamepad = action.bindings[bindingIndex].path.Contains("Gamepad");

        ///if (isGamepad)
        //ControlsRemapping.RemapGamepadAction(action, bindingIndex);
        //else
        action.Disable();
        ControlsRemapping.Controls.Disable();
            ControlsRemapping.RemapKeyboardAction(action, bindingIndex);
    }

    public void OnSuccessfulRebinding(InputAction action)
    {
        ControlsRemapping.SuccessfulRebinding -= OnSuccessfulRebinding;
        SetButtonText();
    }

    public void OnDestroy()
    {
        rebindButton.onClick.RemoveAllListeners();
    }
}