using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.DbEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_Menu: VM_Base
	{
		private List<VM_LanguageItem> _languages;
		private VM_MenuItemList _menuitems;

        public List<VM_LanguageItem> Languages
        {
            get { return _languages; }
            set { _languages = value; }
        }
        public VM_MenuItemList Menuitems
		{
			get { return _menuitems; }
			set { _menuitems = value; }
		}

		public VM_Menu()
		{
			//_languages = new List<VM_LanguageItem>();
			//_menuitems = new VM_MenuItemList();
		}
		public VM_Menu(IList<Language> entityLanguage, IList<V_HMIUserMenu> entityMenu)
		{
            //_languages = new List<VM_LanguageItem>(entityLanguage);
			//_menuitems = new VM_MenuItemList(entityMenu);
		}
	}
	public class VM_LanguageItem:VM_Base
	{
		public VM_LanguageItem()
		{
		}
		public VM_LanguageItem(Language entity)
		{
			this.Code = entity.LanguageCode;
			this.DisplayName = entity.LanguageName;
			this.IconName = entity.IconName;
      this.Order = entity.Order;
		}
		public virtual String Code { get; set; }
		public virtual String DisplayName { get; set; }
		public virtual String IconName { get; set; }
    public virtual int Order { get; set; }
  }
    public class VM_LanguageItemList : List<VM_LanguageItem>
    {
        public VM_LanguageItemList(IList<Language> dbClass)
        {
            foreach (Language item in dbClass)
            {
                this.Add(new VM_LanguageItem(item));
            }
        }
    }
    public class VM_MenuItem : VM_Base
	{
		public VM_MenuItem()
		{
		}
		public VM_MenuItem(V_HMIUserMenu entity)
		{
			this.Id = entity.HmiClientMenuId;
			this.Name = entity.DisplayName;
			this.DisplayName = ResourceController.GetMenuDisplayName(entity.DisplayName);
			this.Controller = entity.Controller;
			this.Method = entity.Method;
			this.ParentId = entity.ParentId; //Changed to ParentId 
			this.DisplayOrder = entity.DisplayOrder;
			this.MethodParameter = entity.MethodParameter;
			this.IconName = entity.IconName;
		}
		public VM_MenuItem(HmiClientMenu entity)
		{
			this.Id = entity.HmiClientMenuId;
			this.Name = entity.DisplayName;
			this.DisplayName = ResourceController.GetMenuDisplayName(entity.DisplayName);/*entity.DisplayName;*/
			this.Controller = entity.Controller;
			this.Method = entity.Method;
			this.ParentId = entity.ParentId; //Changed to ParentId 
			this.DisplayOrder = entity.DisplayOrder;
			this.MethodParameter = entity.MethodParameter;
			this.IconName = entity.IconName;
		}
		public virtual Int64 Id { get; set; }
		public virtual String Name { get; set; }
		public virtual String DisplayName { get; set; }
		public virtual String Controller { get; set; }
		public virtual String Method { get; set; }
		public virtual Int64? ParentId { get; set; }
		public virtual Int32? DisplayOrder { get; set; }
		public virtual String MethodParameter { get; set; }
		public virtual List<VM_MenuItem> Children { get; set; }
		//public virtual Int64 ModuleId { get; set; }
		public virtual String IconName { get; set; }
	}
	public class VM_MenuItemList : List<VM_MenuItem>
	{
        public VM_MenuItemList()
        {
        }
        public VM_MenuItemList(IList<V_HMIUserMenu> dbClass)
        {
            foreach (V_HMIUserMenu item in dbClass)
            {
                this.Add(new VM_MenuItem(item));
            }
        }

        public VM_MenuItemList(IList<HmiClientMenu> dbClass)
        {
            foreach (HmiClientMenu item in dbClass)
            {
                this.Add(new VM_MenuItem(item));
            }
        }

    }
}
