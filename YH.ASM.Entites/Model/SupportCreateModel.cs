using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
    public class SupportCreateModel
    {
        public int CreatorId { get; set; }

        public int ConductorId { get; set; }
        public int ProjectId { get; set; }
        public int Type { get; set; }

        public int Priority { get; set; }
        public int Severity { get; set; }

        public DateTime FindDate { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<FilelList> Filelist { get; set; }
    }

    public class FilelList
    {
        public int ID { get; set; }
        public string FILENAME { get; set; }
        public string URL { get; set; }


    }

}
