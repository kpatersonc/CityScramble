using Caliburn.Micro;

namespace InfusionGames.CityScramble.ViewModels
{
    /// <summary>
    /// BindableCollection of T organized into a named group
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Group<T> : BindableCollection<T>
    {
        public string Name { get; set; }
    }
}
