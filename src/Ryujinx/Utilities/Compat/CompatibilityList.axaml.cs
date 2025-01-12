﻿using Avalonia.Controls;
using Avalonia.Styling;
using Ryujinx.Ava.UI.Helpers;
using System.Threading.Tasks;

namespace Ryujinx.Ava.Utilities.Compat
{
    public partial class CompatibilityList : UserControl
    {
        public static async Task Show()
        {
            await CompatibilityHelper.InitAsync();

            CompatibilityContentDialog contentDialog = new()
            {
                Content = new CompatibilityList { DataContext = new CompatibilityViewModel(RyujinxApp.MainWindow.ViewModel.ApplicationLibrary) }
            };

            Style closeButton = new(x => x.Name("CloseButton"));
            closeButton.Setters.Add(new Setter(WidthProperty, 80d));
            
            Style closeButtonParent = new(x => x.Name("CommandSpace"));
            closeButtonParent.Setters.Add(new Setter(HorizontalAlignmentProperty, Avalonia.Layout.HorizontalAlignment.Right));

            contentDialog.Styles.Add(closeButton);
            contentDialog.Styles.Add(closeButtonParent);

            await ContentDialogHelper.ShowAsync(contentDialog);
        }
        
        public CompatibilityList()
        {
            InitializeComponent();
        }

        private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (DataContext is not CompatibilityViewModel cvm)
                return;

            if (sender is not TextBox searchBox)
                return;
        
            cvm.Search(searchBox.Text);
        }
    }
}
