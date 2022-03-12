using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.Fractals.XamarinApp.PropertyEditViews
{
    public partial class BooleanPropertyEditView : ContentView
    {
        private IFractalProperty m_property = null;

        private Action m_refreshAction = null;

        public BooleanPropertyEditView(IFractalProperty property, Action refreshAction)
        {
            InitializeComponent();

            m_property = property;
            m_refreshAction = refreshAction;

            this.TitleLabel.Text = m_property.DisplayName;
            this.BooleanSwitch.IsToggled = (m_property.BoolValue ?? false);
        }

        void BooleanSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            m_property.BoolValue = e.Value;
            m_refreshAction?.Invoke();
        }
    }
}
