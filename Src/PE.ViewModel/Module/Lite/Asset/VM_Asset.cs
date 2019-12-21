using PE.DbEntity.Enums;
using PE.DbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.ViewModel.Module.Lite.Asset
{
  public class VM_Asset : VM_Base
  {

    [SmfDisplayAttribute(typeof(VM_Asset), "AssetCode", "NAME_AssetCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetCode { get; set; }

    [SmfDisplayAttribute(typeof(VM_Asset), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }


        [SmfDisplayAttribute(typeof(VM_Asset), "IsCheckpoint", "NAME_IsCheckpoint")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public bool IsCheckpoint { get; set; }

        [SmfDisplayAttribute(typeof(VM_Asset), "AssetOrderSeq", "NAME_AssetOrderSeq")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public short AssetOrderSeq { get; set; }

       public long? ParentAssetId { get; set; }

        [SmfDisplayAttribute(typeof(VM_Asset), "Levels", "NAME_Levels")]
        //[DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string Levels { get; set; }


        [SmfDisplayAttribute(typeof(VM_Asset), "Path", "NAME_Path")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public string Path { get; set; }


        [SmfDisplayAttribute(typeof(VM_Asset), "Seq", "Seq")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long? Seq { get; set; }

        [SmfDisplayAttribute(typeof(VM_Asset), "AssetId", "NAME_AssetId")]
        [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
        public long AssetId { get; set; }




        public VM_Asset(V_Assets table)
        {
            Seq = table.Seq;
            ParentAssetId = table.ParentAssetId;
            AssetCode = table.AssetCode;
            AssetName = table.AssetName;
            IsCheckpoint = table.IsCheckpoint;
            AssetOrderSeq = table.AssetOrderSeq;
            Levels = table.Levels;
            Path = table.Path;
            AssetId = table.AssetId;
 

        }

        public VM_Asset()
        {


        }



    }
}
