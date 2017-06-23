using Xamarin.Forms;
using System.Reflection;

namespace InfusionGames.CityScramble.Behaviors
{
    public class ActionSheet 
    {
        #region Parameters
        public static BindableProperty ParametersProperty =
            BindableProperty.CreateAttached("Parameters", typeof(ActionSheetParameters), typeof(ActionSheet), default(ActionSheetParameters));

        public static ActionSheetParameters GetParameters(BindableObject bindable)
        {
            return (ActionSheetParameters)bindable.GetValue(ParametersProperty);
        }

        public static void SetParameters(BindableObject bindable, ActionSheetParameters value)
        {
            bindable.SetValue(ParametersProperty, value);
        }
        #endregion

        #region Result
        public static BindableProperty ResultProperty = 
            BindableProperty.CreateAttached("Result", typeof(string), typeof(ActionSheet), default(string), defaultBindingMode: BindingMode.TwoWay);

        public static string GetResult(BindableObject bindable)
        {
            return (string)bindable.GetValue(ResultProperty);
        }

        public static void SetResult(BindableObject bindable, string value)
        {
            bindable.SetValue(ResultProperty, value);
        }
        #endregion

        #region ShowDialog
        public static BindableProperty ShowDialogProperty =
           BindableProperty.CreateAttached("ShowDialog", typeof(bool), typeof(ActionSheet), default(bool), propertyChanged: OnShowDialog, defaultBindingMode: BindingMode.TwoWay);

        public static bool GetShowDialog(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ResultProperty);
        }

        public static void SetShowDialog(BindableObject bindable, bool value)
        {
            bindable.SetValue(ShowDialogProperty, value);
        } 
        #endregion

        private static async void OnShowDialog(BindableObject bindable, object oldValue, object newValue)
        {
            var page = GetParent<Page>(bindable);

            bool showAlert = (bool)newValue;
            if (showAlert)
            {
                ActionSheetParameters args = GetParameters(bindable);
                if (page != null && args != null)
                {
                    string result = await page.DisplayActionSheet(args.Title, args.Cancel, args.Destruction, args.Buttons);

                    SetResult(bindable, result); // pass result back to binding
                    SetShowDialog(bindable, false); // reset the dialog
                }
            }
        }

        private static T GetParent<T>(BindableObject bindable) where T : class
        {
            if (bindable is T)
            {
                return bindable as T;
            }
            else
            {
                PropertyInfo propInfo = bindable.GetType().GetRuntimeProperty("Parent");
                if (propInfo != null)
                {
                    var parent = propInfo.GetValue(bindable) as BindableObject;
                    if (parent != null)
                    {
                        return GetParent<T>(parent);
                    }
                }
            }

            return default(T);
        }
    }

    public class ActionSheetParameters
    {
        /// <summary>
        /// Action sheet title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Cancel button title
        /// </summary>
        public string Cancel { get; set; }

        /// <summary>
        /// Destructive action title
        /// </summary>
        public string Destruction { get; set; }

        /// <summary>
        /// List of Buttons
        /// </summary>
        public string[] Buttons { get; set; }
    }
}
