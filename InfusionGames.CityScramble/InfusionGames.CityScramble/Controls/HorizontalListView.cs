using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.Controls
{
    public class HorizontalListView : ScrollView
    {
        private readonly StackLayout _imageStack;

        public HorizontalListView()
        {
            Orientation = ScrollOrientation.Horizontal;

            _imageStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            Content = _imageStack;
        }

        public IList<View> Children => _imageStack.Children;

        public DataTemplate ItemTemplate { get; set; }

        #region ItemSource

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemSource", typeof(IList), typeof(HorizontalListView), default(IList), BindingMode.TwoWay,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    ((HorizontalListView) bindableObject).ItemsSourceChanged(bindableObject, oldValue as IList,
                        newValue as IList);
                });

        private void ItemsSourceChanged(BindableObject bindableObject, IList oldValue, IList newValue)
        {
            if (ItemsSource == null)
            {
                return;
            }

            Children.Clear();

            if (newValue.Count > 0)
            {
                AddChildren(newValue);
            }

            var notifyCollection = newValue as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                    {
                        AddChildren(args.NewItems);
                    }
                    if (args.OldItems != null)
                    {
                        RemoveChildren(args.OldItems);
                    }
                };
            }
        }

        private void AddChildren(IList items)
        {
            foreach (var item in items)
            {
                var viewCell = (ViewCell)ItemTemplate.CreateContent();
                viewCell.View.BindingContext = item;
                viewCell.View.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(ViewCellTapped),
                    CommandParameter = viewCell.View.BindingContext
                });
                Children.Add(viewCell.View);
            }
        }

        private void RemoveChildren(IList items)
        {
            var bindingContexts = Children
                .Select(c => c.BindingContext)
                .ToList();

            foreach (var item in items)
            {
                var index = bindingContexts.IndexOf(item);
                Children.RemoveAt(index);
            }
        }

        private void ViewCellTapped(object obj)
        {
            SelectedItem = obj;
        }

        public IList ItemsSource
        {
            get { return (IList) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion
        
        #region SelectedItem

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(HorizontalListView), null, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((HorizontalListView)bindable).UpdateSelectedIndex();
                });

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private void UpdateSelectedIndex()
        {
            if (SelectedItem == BindingContext)
                return;

            SelectedIndex = Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(SelectedItem);

        }

        #endregion
        
        #region SelectedIndex

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create("SelectedIndex", typeof(int), typeof(HorizontalListView), default(int), BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((HorizontalListView)bindable).UpdateSelectedItem();
                });

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        private void UpdateSelectedItem()
        {
            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
        }

        #endregion

    }
}
