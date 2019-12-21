using SMF.DbEntity;
using SMF.DbEntity.Model;
using SMF.Module.Core;
using SMF.RepositoryPattern.Repositories;
using SMF.RepositoryPatternExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace PE.HMIWWW.Core.Parameter
{
	public static class ParameterController
	{
		#region members

		private static List<SMF.DbEntity.Model.Parameter> Parameters { get; set; }
        private static string _groupName = "HMI_WWW";
		#endregion
		#region ctor

		static ParameterController()
		{
			ReadParamaters();
		}

		#endregion

		public static void ReadParamaters()
		{
			try
			{
        using (SMFContext smfCtx = new SMFContext())
        {
          // using (SMFUnitOfWork uow = new SMFUnitOfWork())
          //{

          //IRepository<SMF.DbEntity.Model.Parameter> ipr = uow.Repository<SMF.DbEntity.Model.Parameter>();
          if (String.IsNullOrEmpty(_groupName))
            Parameters = smfCtx.Parameters.Include(i => i.ParameterGroup).Include(i => i.UnitOfMeasure).ToList();
						//Parameters = ipr.Query().Include(a => a.ParameterGroup).Include(b => b.UnitOfMeasure).Get().ToList();
					else
            Parameters = smfCtx.Parameters.Where(x=>x.ParameterGroup.Name==_groupName).Include(i => i.ParameterGroup).Include(i => i.UnitOfMeasure).ToList();
          //Parameters = ipr.Query(x => x.ParameterGroup.Name == _groupName).Include(a => a.ParameterGroup).Include(b => b.UnitOfMeasure).Get().ToList();

				}
			}

			catch (Exception ex)
			{
				DbExceptionHelper.ProcessException(ex, "Error during reading parameters from the DB.");
			}
		}
		public static SMF.DbEntity.Model.Parameter GetParameter(string parameterName)
		{
			if (String.IsNullOrEmpty(parameterName))
				return null;

			return Parameters.FirstOrDefault(p => p.Name == parameterName);
		}
	}
}
