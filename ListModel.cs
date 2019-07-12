using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public enum ActionType
        {
            AddItem,
            RemoveItem
        }
        
        LimitedSizeStack<Tuple<ActionType, TItem, int>> actionStory;
        
        public List<TItem> Items { get; }
        private int limit;       

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            this.limit = limit;
            actionStory = new LimitedSizeStack<Tuple<ActionType, TItem, int>>(limit);
        }

        public void AddItem(TItem item)
        {
            actionStory.Push(Tuple.Create(ActionType.AddItem, item, Items.Count));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            actionStory.Push(Tuple.Create(ActionType.RemoveItem,Items[index],index));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return actionStory.Count > 0;
        }

        public void Undo()
        {
            var lastActoin = actionStory.Pop();
            switch (lastActoin.Item1)
            {
                case ActionType.AddItem:
                    Items.RemoveAt(actionStory.Count);
                    break;
                case ActionType.RemoveItem:
                    Items.Insert(lastActoin.Item3 , lastActoin.Item2);
                    break;
            }
        }
    }
}