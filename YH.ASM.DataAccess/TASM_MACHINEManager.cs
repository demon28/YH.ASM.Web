using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.DataAccess
{
    public class TASM_MACHINEManager :  DbContext<TASM_MACHINE>
    {

        public SimpleClient<TASM_MACHINE> TASM_MACHINERDb { get { return new SimpleClient<TASM_MACHINE>(Db); } }


        public bool ListByWhere(string keyword, ref PageModel p, ref List<MachineModel> list)
        {

            string sql = @"select t.*,
tr.name typesname,
 tp.name PROJECTNAME 
  from tasm_machine t
  left join tasm_machine_type tr
  on t.types = tr.id
  left join tasm_project tp 
  on t.projectid=tp.pid "
;
            int pagecount = 0;
            list = Db.SqlQueryable<MachineModel>(sql).Where(s=>s.NAME.Contains(keyword)  
            ||s.DELIVERYNUMBER.Contains(keyword)
            ||s.CONTRACT.Contains(keyword)
            || s.TYPESNAME.Contains(keyword)).OrderBy(s=>s.MID,OrderByType.Asc).ToPageList(p.PageIndex,p.PageSize, ref pagecount);


          

            p.PageCount = pagecount;

            return list.Count > 0;
        }



        public bool ListByProject(int projectId, string keyword, ref PageModel p, ref List<MachineModel> list)
        {

            string sql = @"select t.*,
tr.name typesname,
 tp.name PROJECTNAME 
  from tasm_machine t
  left join tasm_machine_type tr
  on t.types = tr.id
  left join tasm_project tp 
  on t.projectid=tp.pid "
;
            int pagecount = 0;
            list = Db.SqlQueryable<MachineModel>(sql)
                .Where(s => s.PROJECTID== projectId)
                .Where(s => s.NAME.Contains(keyword)
            || s.DELIVERYNUMBER.Contains(keyword)
            || s.CONTRACT.Contains(keyword)
            || s.TYPESNAME.Contains(keyword))
                .OrderBy(s => s.MID, OrderByType.Asc)
                .ToPageList(p.PageIndex, p.PageSize, ref pagecount);

            p.PageCount = pagecount;

            return list.Count > 0;
        }





        /// <summary>
        /// 不分页，导出excel用
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="list"></param>
        /// <returns></returns>

        public bool ListByWhere(string keyword, ref List<MachineModel> list)
        {
            string sql = @"select t.*,
tr.name typesname,
 tp.name PROJECTNAME 
  from tasm_machine t
  left join tasm_machine_type tr
  on t.types = tr.id
  left join tasm_project tp 
  on t.projectid=tp.pid "
           ;
      
            list = Db.SqlQueryable<MachineModel>(sql).Where(s => s.NAME.Contains(keyword)).OrderBy(s => s.MID, OrderByType.Asc).ToList();

            return list.Count > 0;
        }


        public MachineModel SelectById(int mid) {
            string sql = @"select t.*,
tr.name typesname,
 tp.name PROJECTNAME 
  from tasm_machine t
  left join tasm_machine_type tr
  on t.types = tr.id
  left join tasm_project tp 
  on t.projectid=tp.pid "
         ;

            MachineModel model = Db.SqlQueryable<MachineModel>(sql).Where(s => s.MID == mid).First();
            return model;

        }


        public bool  ListByType(int typeid, ref List<TASM_MACHINE> list)
        {

            list = TASM_MACHINERDb.GetList().Where(s => s.TYPES == typeid).ToList();

            return list.Count > 0;

        }

        public int SelectMachineCount()
        {

            return Db.Queryable<TASM_MACHINE>().Count();
        }

    }
}
