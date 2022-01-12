using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Interaction Elements")]
    [SerializeField] 
    private UiCell m_SelectedCell;
    public UiCell SelectedCell
    {
        get { return m_SelectedCell; }
        set
        {
            m_SelectedCell = value;

            editButton.interactable = value != null;
            deleteButton.interactable = value != null;
        }
    }
    [SerializeField] 
    private CellCategory m_SelectedCategory;
    public CellCategory SelectedCategory
    {
        get { return m_SelectedCategory; }
        set
        {
            m_SelectedCategory = value;

            // Instantiate grid with new category or new order
            InstantiateGrid(m_SelectedCategory, CategoryOrder);

            // Reset buttons that arent that category
            foreach (var item in categoryButtons)
            {
                if (item.category != m_SelectedCategory)
                    item.CellsOrder = Order.None;
            }
        }
    }
    public Order CategoryOrder;
    public Button addButton;
    public Button editButton;
    public Button deleteButton;
    public ClientInfoPanel clientInfoPanel;
    public List<CategoryButton> categoryButtons;

    [Header("Prefabs")]
    [SerializeField] private UiCell m_CellPrefab;

    [Header("Grid Elements")]
    public Transform cellsParent;
    public Transform gridWindow;
    public GridLayoutGroup gridLayoutGroup;

    [Header("Grid Cells")]
    [SerializeField] private List<UiCell> m_Cells = new List<UiCell>();
    [SerializeField] private List<CellInfo> m_Info = new List<CellInfo>();

    private void Awake()
    {
        // Get cell prefab
        m_CellPrefab = Resources.Load<UiCell>("Prefabs/UI/Cell");

        // Setup buttons Bheaviours
        addButton.onClick.AddListener(AddButtonBehaviour);
        editButton.onClick.AddListener(EditButtonBehaviour);
        deleteButton.onClick.AddListener(DeleteButtonBehaviour);

        // Initialize test data
        m_Info.Add(new CellInfo("000001", "Lucas", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000002", "Ana", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000003", "Joana", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000004", "Renato", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000005", "Paulo", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
    }

    private void Start()
    {
        // Set buttons non-interactable at the start
        editButton.interactable = false;
        deleteButton.interactable = false;

        // Set default order and category
        categoryButtons[0].CellsOrder = Order.Ascending;
    }

    private void LateUpdate()
    {
        // Set cell settings - // needed so cell adjusts size when changing window size
        gridLayoutGroup.cellSize = new Vector2(gridWindow.GetComponent<RectTransform>().rect.width, 50f);
    }

    public void InstantiateGrid(CellCategory category, Order order)
    {
        // Clear Selected Cell
        SelectedCell = null;

        // Start with a blank grid
        ClearGrid();

        // No info to initialize
        if (m_Info.Count == 0)
            return;

        // Get Info in order
        var infoInOrder = m_Info.ToArray();
        switch (category)
        {
            case CellCategory.Id:
                Array.Sort(infoInOrder, (x, y) => order == Order.Ascending
                    ? StringComparer.OrdinalIgnoreCase.Compare(x.id, y.id)
                    : -StringComparer.OrdinalIgnoreCase.Compare(x.id, y.id));
                break;
            case CellCategory.ClientName:
                Array.Sort(infoInOrder, (x, y) => order == Order.Ascending
                   ? StringComparer.OrdinalIgnoreCase.Compare(x.clientName, y.clientName)
                   : -StringComparer.OrdinalIgnoreCase.Compare(x.clientName, y.clientName));
                break;
            case CellCategory.PhoneNumber:
                Array.Sort(infoInOrder, (x, y) => order == Order.Ascending
                   ? StringComparer.OrdinalIgnoreCase.Compare(x.phoneNumber, y.phoneNumber)
                   : -StringComparer.OrdinalIgnoreCase.Compare(x.phoneNumber, y.phoneNumber));
                break;
            case CellCategory.Address:
                Array.Sort(infoInOrder, (x, y) => order == Order.Ascending
                   ? StringComparer.OrdinalIgnoreCase.Compare(x.address, y.address)
                   : -StringComparer.OrdinalIgnoreCase.Compare(x.address, y.address));
                break;
            case CellCategory.Email:
                Array.Sort(infoInOrder, (x, y) => order == Order.Ascending
                   ? StringComparer.OrdinalIgnoreCase.Compare(x.email, y.email)
                   : -StringComparer.OrdinalIgnoreCase.Compare(x.email, y.email));
                break;
            case CellCategory.Nif:
                Array.Sort(infoInOrder, (x, y) => order == Order.Ascending
                   ? StringComparer.OrdinalIgnoreCase.Compare(x.nif, y.nif)
                   : -StringComparer.OrdinalIgnoreCase.Compare(x.nif, y.nif));
                break;
            default:
                break;
        }

        // Instantiate cells
        for (int i = 0; i < infoInOrder.Length; i++)
        {
            var info = infoInOrder[i];
            var cell = Instantiate(m_CellPrefab, cellsParent);
            cell.Initialize(info, info.id, info.clientName, info.phoneNumber,info.address, info.email, info.nif, this);
            cell.name = info.id;

            m_Cells.Add(cell);
        }
    }

    public void AddInfo(CellInfo info)
    {
        m_Info.Add(info);
        InstantiateGrid(SelectedCategory, CategoryOrder);
    }

    public void ClearSelectedCell()
    {
        SelectedCell = null;
    }
    private void ClearGrid()
    {
        foreach (var item in m_Cells)
        {
            Destroy(item.gameObject);
        }

        m_Cells = new List<UiCell>();
    }

    private void AddButtonBehaviour()
    {
        clientInfoPanel.gameObject.SetActive(true);
        clientInfoPanel.ClearInfo();
        clientInfoPanel.Function = InfoPanelFuncion.Add;
    }
    private void EditButtonBehaviour()
    {
        clientInfoPanel.gameObject.SetActive(true);
        clientInfoPanel.SetInfo(SelectedCell.info);
        clientInfoPanel.Function = InfoPanelFuncion.Edit;
    }
    private void DeleteButtonBehaviour()
    {
        Debug.Log("Funcion not implemented yet");
    }

}