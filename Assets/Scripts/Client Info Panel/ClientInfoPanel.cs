using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InfoPanelFuncion
{
    Add,
    Edit
}

public class ClientInfoPanel : MonoBehaviour
{
    [SerializeField] private InfoPanelFuncion m_Function;
    public InfoPanelFuncion Function
    {
        get { return m_Function; }
        set 
        { 
            m_Function = value;

            // Change bottom button text
            confirmButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value == InfoPanelFuncion.Add ? "Adicionar" : "Editar";
            // Make id input non interactable if editing info
            idInput.interactable = m_Function == InfoPanelFuncion.Add ? true : false;
        }
    }

    public TMP_InputField idInput;
    public TMP_InputField nameInput;
    public TMP_InputField phoneNumberInput;
    public TMP_InputField addressInput;
    public TMP_InputField emailInput;
    public TMP_InputField nifInput;
    public TMP_InputField observationsInput;
    public MainMenuController mainMenuController;
    public Button cancelButton;
    public Button printButton;
    public Button confirmButton;

    private CellInfo CurrentInfo
    {
        get 
        {
            return new CellInfo(idInput.text, nameInput.text, phoneNumberInput.text,addressInput.text, emailInput.text, nifInput.text, observationsInput.text); 
        }
    }

    private void Awake()
    {
        // Panel disabled on start
        gameObject.SetActive(false);

        // Setup buttons Bheaviours
        cancelButton.onClick.AddListener(CancelButtonBehaviour );
        printButton.onClick.AddListener(PrintButtonBehaviour);
        confirmButton.onClick.AddListener(ConfirmButtonBehaviour);
    }

    public void SetInfo(CellInfo info)
    {
        idInput.text = info.id;
        nameInput.text = info.clientName;
        phoneNumberInput.text = info.phoneNumber;
        addressInput.text = info.address;
        emailInput.text = info.email;
        nifInput.text = info.nif;
        observationsInput.text = info.observations;
    }
    public void ClearInfo()
    {
        nameInput.text = string.Empty;
        addressInput.text = string.Empty;
        emailInput.text = string.Empty;
        nifInput.text = string.Empty;
        observationsInput.text = string.Empty;
    }

    private void CancelButtonBehaviour()
    {
        gameObject.SetActive(false);
        mainMenuController.ClearSelectedCell();
    }
    private void PrintButtonBehaviour()
    {
        gameObject.SetActive(false);
        mainMenuController.ClearSelectedCell();
    }
    private void ConfirmButtonBehaviour()
    {
        gameObject.SetActive(false);
        mainMenuController.ClearSelectedCell();

        switch (Function)
        {
            case InfoPanelFuncion.Add:
                mainMenuController.AddInfo(CurrentInfo);
                break;
            case InfoPanelFuncion.Edit:
                mainMenuController.EditInfo(CurrentInfo);
                break;
            default:
                break;
        }
    }
}