using PE.HMIWWW.Core.Resources;
using SMF.HMIWWW.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace PE.HMIWWW.ViewModel.System
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
        [SmfDisplayAttribute(typeof(VM_Account), "Email", "NAME_Email")]
        [EmailAddress(ErrorMessageResourceName = "FORM_ATTRIBUTE_Email", ErrorMessageResourceType = typeof(VM_Resources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
        [SmfDisplayAttribute(typeof(VM_Account), "Password", "NAME_Password")]
        [StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources), MinimumLength = 6)]
        public string Password { get; set; }

        [SmfDisplayAttribute(typeof(VM_Account), "RememberMe", "NAME_RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
        [SmfDisplayAttribute(typeof(VM_Account), "Email", "NAME_Email")]
        [EmailAddress(ErrorMessageResourceName = "FORM_ATTRIBUTE_Email", ErrorMessageResourceType = typeof(VM_Resources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
        [SmfDisplayAttribute(typeof(VM_Account), "Password", "NAME_Password")]
        [StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [SmfDisplayAttribute(typeof(VM_Account), "ConfirmPassword", "NAME_PasswordConfirm")]
        [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessageResourceName = "FORM_ATTRIBUTE_WrongStringLength", ErrorMessageResourceType = typeof(VM_Resources), MinimumLength = 6)]
        [Compare("Password", ErrorMessageResourceName = "FORM_ATTRIBUTE_PasswordsDoNotMatch", ErrorMessageResourceType = typeof(VM_Resources))]
        public string ConfirmPassword { get; set; }


        [SmfDisplayAttribute(typeof(VM_Account), "Language", "NAME_Language")]
        public long Language { get; set; }
    }
}
