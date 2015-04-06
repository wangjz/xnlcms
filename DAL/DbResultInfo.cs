using System;
using System.Collections.Generic;
using System.Text;

namespace COM.SingNo.DAL
{
   public class DbResultInfo
    {
       public List<object> value;
       public Exception exception;
       public bool isSuccess ; 
       public DbResultInfo()
		{
		}
       public DbResultInfo(bool isSuccess, List<object> value)
       {
           this.isSuccess = isSuccess;
           this.value = value;
       }
       public DbResultInfo(bool isSuccess, List<object> value, Exception e)
       {
           this.isSuccess = isSuccess;
           this.value = value;
           this.exception = e;
       }
    }
   public enum DataBaseType
   {
       sqlServer,
       Oracle,
       Access,
       FireBird,
       MySql
   }
}
