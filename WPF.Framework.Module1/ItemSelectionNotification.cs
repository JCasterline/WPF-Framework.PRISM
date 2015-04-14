using System.Collections.Generic;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace WPF.Framework.Module1
{
    public class ItemSelectionNotification : Confirmation
    {
        public ItemSelectionNotification()
        {
            Items = new List<string>();
            SelectedItem = null;
        }

        public ItemSelectionNotification(IEnumerable<string> items)
            : this()
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public IList<string> Items { get; private set; }

        public string SelectedItem { get; set; }
    }
}
