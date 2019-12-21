using SMF.RepositoryPattern.Ef6;

namespace PE.DbEntity.Model
{
	public class PropertyEntityBase : EntityBase
	{
		public long PropertyId { get; set; } // PropertyId
		public long OwnerId { get; set; } // OwnerId
		public string Name { get; set; } // Name
		public string Value { get; set; } // Value
	}
}