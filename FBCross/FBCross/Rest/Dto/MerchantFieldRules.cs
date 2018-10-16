using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class MerchantFieldRules
    {
        public StandardFieldRules StandardFieldRules { get; set; }
        public List<CustomField> CustomFields { get; set; }
        public string Error { get; set; }
    }

    public class CustomField
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool IsRequired { get; set; }
        public bool IsRequiredWidget { get; set; }
        public string MergeTag { get; set; }
        public int PlacementOrder { get; set; }
        public CustomFieldType FieldType { get; set; }
        public List<string> DropDownOptions { get; set; }
        public bool ShowOtherOption { get; set; }
        public List<int> ServiceIds { get; set; }
        public List<int> EmployeeIds { get; set; }
        public bool SaveToCustomerRecord { get; set; }
        public bool HideFromBookingForm { get; set; }
        public bool HideFromAdminForm { get; set; }
    }

    public enum CustomFieldType
    {
        TextField,
        NumberField,
        DateField,
        DropDown,
        LargeTextField,
        MultiCheckbox
    }

    public class StandardFieldRules
    {
        public bool HideFirstName { get; set; }
        public bool HideLastName { get; set; }
        public bool HideNotes { get; set; }
        public bool HideEmail { get; set; }
        public bool HidePhone { get; set; }
        public string FirstNameCaption { get; set; }
        public string LastNameCaption { get; set; }
        public string EmailCaption { get; set; }
        public string PhoneCaption { get; set; }
        public string NotesCaption { get; set; }
        public int FirstNameOrderPlacement { get; set; }
        public int LastNameOrderPlacement { get; set; }
        public int EmailOrderPlacement { get; set; }
        public int PhoneOrderPlacement { get; set; }
        public int NotesOrderPlacement { get; set; }
        public bool FirstNameRequired { get; set; }
        public bool LastNameRequired { get; set; }
        public bool NotesRequired { get; set; }
        public bool EmailRequired { get; set; }
        public bool PhoneRequiredWidget { get; set; }
        public bool FirstNameRequiredWidget { get; set; }
        public bool LastNameRequiredWidget { get; set; }
        public bool NotesRequiredWidget { get; set; }
        public bool EmailRequiredWidget { get; set; }
        public bool PhoneRequired { get; set; }
        public bool EitherEmailOrPhoneRequired { get; set; }

        public bool EitherEmailOrPhoneRequiredWidget { get; set; }
        public bool DisplayNumberOfAttendees { get; set; }
        public int NumberOfAttendeesPlacement { get; set; }
        public string NumberOfAttendeesCaption { get; set; }
    }

    
}
