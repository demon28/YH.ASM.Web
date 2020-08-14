using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.DataAccess
{
   public  class TASM_PROJECTManager : DbContext<TASM_PROJECT>
    {


        public bool ListByWhere(string keywords, ref PageModel p, ref List<ProjectModel> list) {

            string sql = @"
select t.*,
       tt.id          typesid,
       tt.name        TYPESNAME,
       tt.installdays,
       tt.debugdays,
       tt.checkdays,
       tarea.area_name
       
  from tasm_project t
  left join tasm_machine_type tt
    on t.machinetype = tt.id
  left join tnet_area tarea
    on t.provinceid=tarea.area_id
";

            int pagecount = 0;
            list= Db.SqlQueryable<ProjectModel>(sql)
                .Where(s => s.NAME.Contains(keywords))
                .OrderBy(s => s.PID, OrderByType.Desc)
                .ToPageList(p.PageIndex, p.PageSize, ref pagecount);

            p.PageCount = pagecount;
            return list.Count > 0;
        
        }


        public bool ListByWhere(string keywords,  ref List<ProjectModel> list)
        {

            string sql = @"select t.* , tt.id typesid,tt.name typesname, tm.mid macid, tm.name  macname from tasm_project t
 left join tasm_machine_type tt on t.machinetype= tt.id
 left join tasm_machine tm on t.machinet=tm.mid ";

            list = Db.SqlQueryable<ProjectModel>(sql).Where(s => s.NAME.Contains(keywords)).OrderBy(s=>s.PID,OrderByType.Desc).ToList();

            return list.Count > 0;




        }


        public int SelectProjectCount()
        {
            return Db.Queryable<TASM_PROJECT>().Count();
        }

        public TASM_PROJECT SelectByName(string name) {

           


            return Db.Queryable<TASM_PROJECT>().Where(s => s.NAME.Contains(name)).First();

        }





    }
}
