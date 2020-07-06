using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YH.ASM.Web.Models
{
    public class ListSupportInputModel:ApiListModelBase
    {

       
       public int Uuid { get; set; }

      public int  WatchState { get; set; }
        public int WatchType{ get; set; }
    }
}
