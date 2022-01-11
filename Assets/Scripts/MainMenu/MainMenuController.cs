using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Interaction Elements")]
    public UiCell selectedCell;

    [Header("Prefabs")]
    [SerializeField] private UiCell m_CellPrefab;

    [Header("Grid Elements")]
    public Transform cellsParent;
    public GridLayoutGroup gridLayoutGroup;

    [Header("Grid Cells")]
    [SerializeField] private List<UiCell> m_Cells = new List<UiCell>();
    [SerializeField] private List<CellInfo> m_Info = new List<CellInfo>();

    private void Awake()
    {
        // Get cell prefab
        m_CellPrefab = Resources.Load<UiCell>("Prefabs/UI/Cell");

        // Initialize test data
        m_Info.Add(new CellInfo("000001", "Lucas", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000002", "Ana", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000003", "Joana", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000004", "Renato", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
        m_Info.Add(new CellInfo("000005", "Paulo", "967434301", "Estrada Nacional 13", "joaosantosprofi@gmail.com", "260080217", "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum"));
    }

    public void InstantiateGrid(CellCategory category, Order order)
    {
        // Start with a blank grid
        ClearGrid();

        // No info to initialize
        if (m_Info.Count == 0)
            return;

        // Set cell settings
        gridLayoutGroup.cellSize = new Vector2(Screen.width, 50f);

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
            cell.Initialize(info.id, info.clientName, info.phoneNumber,info.address, info.email, info.nif);

            m_Cells.Add(cell);
        }
    }
    private void ClearGrid()
    {
        foreach (var item in m_Cells)
        {
            Destroy(item.gameObject);
        }

        m_Cells = new List<UiCell>();
    }
}