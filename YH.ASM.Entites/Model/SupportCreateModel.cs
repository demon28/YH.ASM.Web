using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Entites.Model
{
    public class SupportCreateModel
    {
        [Required]
        public int CreatorId { get; set; }
       
        /// <summary>
        /// 处理人，最后的处理人不能为可能为空
        /// </summary>
        public int ConductorId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public int ProjectCode { get; set; }
        public int Type { get; set; }

        [Required]
        public int Severity { get; set; }

        public DateTime FindDate { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public int Mid{ get; set; }

        public List<FileInfo> Filelist { get; set; }

        public TASM_SUPPORT_PUSH Push{ get; set; }
    }

    public class FileInfo
    {
        public int ID { get; set; }
        public string FILENAME { get; set; }
        public string URL { get; set; }


    }

}
