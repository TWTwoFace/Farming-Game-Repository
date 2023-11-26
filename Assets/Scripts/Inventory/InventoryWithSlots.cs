using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots : MonoBehaviour, IInventory
{
    public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
    public event Action<object, Type, int> OnInventoryItemRemovedEvent;

    public int capacity { get; set; }
    public bool isFull => _slots.All(slot => slot.isFull);

    private List<IInventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        this.capacity = capacity;
        _slots = new List<IInventorySlot>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }
    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();

        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
                allItems.Add(slot.item);
        }
        return allItems.ToArray();
    }
    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();

        foreach (var slot in _slots)
        {
            if (!slot.isEmpty && slot.itemType == itemType)
                allItemsOfType.Add(slot.item);
        }

        return allItemsOfType.ToArray();
    }
    public IInventoryItem[] GetEquippedItems()
    {
        var allEquippedItems = new List<IInventoryItem>();

        foreach (var slot in _slots)
        {
            if (!slot.isEmpty && slot.item.state.isEquipped)
                allEquippedItems.Add(slot.item);
        }

        return allEquippedItems.ToArray();
    }
    public int GetItemAmount(Type itemType)
    {
        int amount = 0;
        var items = GetAllItems(itemType);
        foreach (var item in items)
        {
            amount += item.state.amount;
        }

        return amount;
    }

    private bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.amount + item.state.amount <= item.info.maxItemsInInventorySlot;
        var amountToAdd = fits ? item.state.amount : item.info.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.state.amount - amountToAdd;

        var clonedItem = item.Clone();
        clonedItem.state.amount = amountToAdd;

        if (slot.isEmpty)
        {
            slot.SetItem(clonedItem);
        }
        else
        {
            slot.item.state.amount += amountToAdd;
        }

        OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);

        if(amountLeft <= 0)
        {
            return true;
        }

        item.state.amount = amountLeft;

        return TryToAdd(sender, item);
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotFull = _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);
        if (slotWithSameItemButNotFull != null)
        {
            return TryToAddToSlot(sender, slotWithSameItemButNotFull, item);
        }

        var emptySlot = _slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
        {
            return TryToAddToSlot(sender, emptySlot, item);
        }

        Debug.Log($"Cannot add ({item.type}), amount: ({item.state.amount}), becaus inventory is full");
        return false;
    }
    
    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = GetAllSlots(itemType);
        if (slotsWithItem.Length == 0)
        {
            return;
        }
        var amountToRemove = amount;
        var count = slotsWithItem.Length;

        for (int i = count - 1; i >= 0; i--)
        {
            var slot = slotsWithItem[i];
            if (slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if (slot.amount <= 0)
                {
                    slot.Clear();
                }

                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);

                break;
            }
            var amountRemoved = amountToRemove;

            amountToRemove -= slot.amount;
            slot.Clear();
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);

        }
    }
    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);
        return item != null;
    }

    private IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
    }
    private IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
}
