using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NG.Fractals.XamarinApp.PropertyEditViews
{
    public partial class IntegerPropertyEditView : ContentView
    {
        private IFractalProperty m_property = null;

        private Action m_refreshAction = null;

        public IntegerPropertyEditView(IFractalProperty property, Action refreshAction)
        {
            InitializeComponent();

            m_property = property;
            m_refreshAction = refreshAction;

            this.TitleLabel.Text = m_property.DisplayName;
            this.ValueLabel.Text = (m_property.IntValue ?? 0).ToString();
        }

        void MinusButton_Clicked(object sender, EventArgs e)
        {
            int newValue = Math.Max(0, (m_property.IntValue ?? 0) - 1);
            m_property.IntValue = newValue;
            this.ValueLabel.Text = newValue.ToString();
            m_refreshAction?.Invoke();
        }

        void PlusButton_Clicked(object sender, EventArgs e)
        {
            int newValue = (m_property.IntValue ?? 0) + 1;
            m_property.IntValue = newValue;
            this.ValueLabel.Text = newValue.ToString();
            m_refreshAction?.Invoke();
        }
    }
}
