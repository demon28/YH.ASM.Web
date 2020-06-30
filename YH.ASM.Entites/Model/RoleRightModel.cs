using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
    public class RoleRightModel
    {

        public int ROLE_ID { get; set; }
        public int PAGE_ID { get; set; }
        public string PAGE_NAME { get; set; }
        public string PAGE_URL { get; set; }
        public int APP_ID { get; set; }
        public string AREA_NAME { get; set; }
        public string CONTROLLER_NAME { get; set; }
        public string ACTION_NAME { get; set; }
        public string OPTION_VALUE { get; set; }

    }
}
