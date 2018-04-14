using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Platform.BusinessEntity
{
   public class ModelBase
   {
       public ModelBase()
       {
           CreateTime = DateTime.Now;
       }

       public virtual int ID { get; set; }
       public virtual DateTime CreateTime { get; set; }
   }
}