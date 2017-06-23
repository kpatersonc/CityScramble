using Xamarin.Forms;

namespace InfusionGames.CityScramble.Behaviors
{
    /// <summary>
    /// Prevents ListView items from selecting items
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.ListView}" />
    public class DisableListViewSelection : Behavior<ListView>
    {
        private ListView _attached;

        protected override void OnAttachedTo(ListView bindable)
        {
            _attached = bindable;

            if (_attached != null)
            {
                _attached.ItemSelected += Bindable_ItemSelected;
            }
            base.OnAttachedTo(bindable);
        }


        protected override void OnDetachingFrom(ListView bindable)
        {
            if (_attached != null)
            {
                _attached.ItemSelected -= Bindable_ItemSelected;
            }
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _attached.SelectedItem = null;
        }
    }
}
