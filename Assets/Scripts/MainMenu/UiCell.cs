using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiCell : MonoBehaviour , IDeselectHandler
{
    public TextMeshProUGUI id;
    public TextMeshProUGUI clientName;
    public TextMeshProUGUI phoneNumber;
    public TextMeshProUGUI address;
    public TextMeshProUGUI email;
    public TextMeshProUGUI nif;

    private Button m_Button;
    [HideInInspector] public MainMenuController controller;

    public void Initialize(string id, string clientName, string phoneNumber, string address, string email, string nif, MainMenuController controller)
    {
        this.id.text = id;
        this.clientName.text = clientName;
        this.phoneNumber.text = phoneNumber;
        this.address.text = address;
        this.email.text = email;
        this.nif.text = nif;
        this.controller = controller;
    }

    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        controller.SelectedCell = this;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        controller.SelectedCell = null;
    }
}
