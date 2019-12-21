using PE.DbEntity.Model;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.System
{
	public class VM_ScheduleItems : VM_Base
	{
		public VM_ScheduleItems(long id, string name, string color)
		{
			Id = id;
			Name = name;
			Color = color;
		}

		#region properties	
		public virtual string Name { get; set; }
		public virtual long Id { get; set; }
		public virtual string Color { get; set; }
		#endregion
	}
}

