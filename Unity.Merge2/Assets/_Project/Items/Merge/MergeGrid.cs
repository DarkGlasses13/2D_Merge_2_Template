using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Items.Merge
{
    public class MergeGrid
    {
        public event Action OnChanged;
        public event Action<Item> OnAdded;

        private readonly Item[] _items;

        public IReadOnlyCollection<Item> Items => _items;
        public bool HasEmptySlots => _items.Contains(null);

        public MergeGrid(int capacity)
        {
            _items = new Item[capacity];
        }

        public bool TryAdd(Item item)
        {
            if (item == null)
                return false;

            if (TryGetEmptySlot(out int emptySlot))
            {
                item.IsInUse = true;
                _items[emptySlot] = item;
                OnAdded?.Invoke(item);
                OnChanged?.Invoke();
                return true;
            }

            return false;
        }

        public bool TryAdd(Item item, int slot)
        {
            if (_items[slot] != null)
                return false;

            item.IsInUse = true;
            _items[slot] = item;
            OnAdded?.Invoke(item);
            OnChanged?.Invoke();
            return true;
        }

        public void Put(int slot, Item item)
        {
            if (_items[slot] != null)
                return;

            item.IsInUse = true;
            _items[slot] = item;
            OnAdded?.Invoke(item);
            OnChanged?.Invoke();
        }

        public void Remove(int slot)
        {
            if (_items[slot] == null)
                return;

            _items[slot].IsInUse = false;
            _items[slot] = null;
            OnChanged?.Invoke();
        }

        public void Swap(int from, int to)
        {
            if (_items[from] == null)
                return;

            Item fromItem = _items[from];
            Item toItem = _items[to];
            _items[from] = toItem;
            _items[to] = fromItem;
            OnChanged?.Invoke();
        }

        public bool TryGetEmptySlot(out int emptySlot)
        {
            if (HasEmptySlots == false)
            {
                emptySlot = 0;
                return false;
            }

            emptySlot = Array.IndexOf(_items, _items.First(slot => slot == null));
            return true;
        }
    }
}
