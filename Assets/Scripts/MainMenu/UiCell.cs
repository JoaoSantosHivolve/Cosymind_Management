using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCell : MonoBehaviour
{
    public TextMeshProUGUI id;
    public TextMeshProUGUI clientName;
    public TextMeshProUGUI phoneNumber;
    public TextMeshProUGUI address;
    public TextMeshProUGUI email;
    public TextMeshProUGUI nif;

    private Button m_Button;
    [HideInInspector] public MainMenuController controller;

    public void Initialize(string id, string clientName, string phoneNumber, string address, string email, string nif)
    {
        this.id.text = id;
        this.clientName.text = clientName;
        this.phoneNumber.text = phoneNumber;
        this.address.text = address;
        this.email.text = email;
        this.nif.text = nif;
    }

    private void Awake()
    {
        m_Button = transform.GetChild(0).GetComponent<Button>();
        m_Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        controller.selectedCell = this;
    }
}
