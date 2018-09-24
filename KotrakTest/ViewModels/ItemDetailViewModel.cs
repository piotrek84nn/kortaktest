using System;

namespace KotrakTest
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Person Item { get; set; }
        public ItemDetailViewModel(Person item = null)
        {
            if (item != null)
            {
                Item = item;
            }
        }
    }
}
