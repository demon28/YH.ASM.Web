using System;
using System.Linq;
using System.Text;
using SqlSugar;


namespace YH.ASM.Entites.CodeGenerator
{
    /// <summary>
    ///  地理基础数据表(省市区)
    ///</summary>
    public class   TNET_AREA
    {

       public TNET_AREA()
       {
      
       }

        ///<summary>
        ///描述：PK
        ///</summary>
     
        public int Area_Id { get; set; }
        ///<summary>
        ///描述：编码身份证前6位
        ///</summary>
        public string Area_Code { get; set; }
        ///<summary>
        ///描述：区域名称。如“广西”
        ///</summary>
        public string Area_Name { get; set; }
        ///<summary>
        ///描述：父ID
        ///</summary>
        public int? Parent_Id { get; set; }
        ///<summary>
        ///描述：行政级别:国=0, 省=1,市=2,区县=3。总是等于parent.area_level+1
        ///</summary>
        public int? Area_Level { get; set; }
        ///<summary>
        ///描述：直观级别。如直辖市、地级市、县级市同视为“市”级
        ///</summary>
        public int? Direct_Level { get; set; }
        ///<summary>
        ///描述：先后顺序
        ///</summary>
        public int? Area_Order { get; set; }
        ///<summary>
        ///描述：区域全称。如“广西壮族自治区”
        ///</summary>
        public string Area_Long_Name { get; set; }

    }
 }








