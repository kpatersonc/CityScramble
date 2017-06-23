using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;

namespace InfusionGames.CityScramble.ViewModels
{

    public static class BindableCollectionExtensions
    {
        // GroupInto?

        public static BindableCollection<Group<TSource>> AddGroupIfNotEmpty<TSource>(this BindableCollection<Group<TSource>> source, Group<TSource> group)
        {
            return AddGroup(source, group, (i) => group.Count > 0);
        }

        public static BindableCollection<Group<TSource>> AddGroup<TSource>(this BindableCollection<Group<TSource>> source, Group<TSource> group, Func<Group<TSource>, bool> predicate)
        {
            if (predicate.Invoke(group))
            {
                source.Add(group);
            }

            return source;
        }

        public static void Sort<TSource, TKey>(this BindableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sorted = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var item in sorted)
            {
                source.Add(item);
            }
        }

        public static void SortDescending<TSource, TKey>(this BindableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sorted = source.OrderByDescending(keySelector).ToList();
            source.Clear();
            foreach (var item in sorted)
            {
                source.Add(item);
            }
        }
    }
}
