using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assignment.Inventory.ItemSlot
{
    public interface IIconHandler : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        event UnityAction<ISlot, List<GameObject>> OnItemDrop;
    }
}