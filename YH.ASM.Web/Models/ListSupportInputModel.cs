using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YH.ASM.Entites;

namespace YH.ASM.Web.Models
{
    public class ListSupportInputModel:ApiListModelBase
    {

       
       public int Uuid { get; set; }

       public SupprotWatchState WatchState { get; set; }
       public SupprotWatchType WatchType { get; set; }
    }
}
