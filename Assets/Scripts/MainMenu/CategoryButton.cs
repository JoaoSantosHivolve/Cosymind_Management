using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CellCategory
{
    Id,
    ClientName,
    PhoneNumber,
    Address,
    Email,
    Nif
}

public enum Order
{
    None,
    Ascending,
    Descending
}

public class CategoryButton : MonoBehaviour, IDeselectHandler, IPointerClickHandler
{
    public CellCategory category;
    private Order m_CellsOrder;
    public Order CellsOrder
    {
        get { return m_CellsOrder; }
        set
        {
            // Set value
            m_CellsOrder = value;

            // Change arrow icon accordably
            switch (m_CellsOrder)
            {
                case Order.None:
                    m_ArrowIcon.sprite = null;
                    m_ArrowIcon.color = Color.clear;
                    break;
                case Order.Ascending:
                    m_ArrowIcon.sprite = m_ArrowUpSprite;
                    m_ArrowIcon.color = m_OriginalColor;
                    break;
                case Order.Descending:
                    m_ArrowIcon.sprite = m_ArrowDownSprite;
                    m_ArrowIcon.color = m_OriginalColor;
                    break;
            }

            // Change Grid order
            if (m_CellsOrder != Order.None)
            {
                controller.InstantiateGrid(category, m_CellsOrder);
            }
        }
    }

    public MainMenuController controller;

    private Image m_ArrowIcon;
    private Color m_OriginalColor;
    private Sprite m_ArrowUpSprite, m_ArrowDownSprite;

    private void Start()
    {
        // Get icon image component
        m_ArrowIcon = transform.GetChild(1).GetComponent<Image>();

        // Get original color
        m_OriginalColor = m_ArrowIcon.color;

        // Get sprite data
        m_ArrowUpSprite = Resources.Load<Sprite>("Images/Icons/Arrow_Up");
        m_ArrowDownSprite = Resources.Load<Sprite>("Images/Icons/Arrow_Down");

        // Set default order
        if (category == CellCategory.Id)
            CellsOrder = Order.Ascending;
        else
            CellsOrder = Order.None;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Set ascending order
        if (CellsOrder == Order.None) CellsOrder = Order.Ascending;
        else if (CellsOrder == Order.Ascending) CellsOrder = Order.Descending;
        else if (CellsOrder == Order.Descending) CellsOrder = Order.Ascending;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        CellsOrder = Order.None;
    }
}