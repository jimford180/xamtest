using FBCross.Rest.Dto;
using FBCross.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FBCross.Components.Customer
{
    public class CustomFieldTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringFieldTemplate { get; set; }
        public DataTemplate NumberFieldTemplate { get; set; }
        public DataTemplate DateFieldTemplate { get; set; }
        public DataTemplate DropDownTemplate { get; set; }
        public DataTemplate LargeTextFieldTemplate { get; set; }
        public DataTemplate MultiCheckboxTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var field = (UnifiedField)item;
            DataTemplate template = null;
            switch (field.FieldType)
            {
                case CustomFieldType.DateField:
                    template = DateFieldTemplate;
                    break;
                case CustomFieldType.DropDown:
                    template = DropDownTemplate;
                    break;
                case CustomFieldType.LargeTextField:
                    template = LargeTextFieldTemplate;
                    break;
                case CustomFieldType.MultiCheckbox:
                    template = MultiCheckboxTemplate;
                    break;
                case CustomFieldType.NumberField:
                    template = NumberFieldTemplate;
                    break;
                case CustomFieldType.TextField:
                    template = StringFieldTemplate;
                    break;
            }
            return template;

        }
    }
}
