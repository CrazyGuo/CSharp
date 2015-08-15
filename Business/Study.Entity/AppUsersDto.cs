using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Study.Entity
{
   public class AppUsersDto
   {
       public int  Id { get; set; } 
       public string  BadgeNo { get; set; } 
       public string  Name { get; set; } 
       public string  Remark { get; set; } 
       public bool  IsAvailable { get; set; } 
       public string  LogOnName { get; set; } 
       public string  EmailAddress { get; set; } 
       public int?  AppAuthTypeId { get; set; } 
       public bool  IsLocked { get; set; } 
       public string  LocalPassword { get; set; } 
       public DateTime?  LocalPasswordSetTime { get; set; } 
       public bool  IsMustChangeLocalPassword { get; set; } 
       public string  RfidNo { get; set; } 
       public string  PssUserNo { get; set; } 
       public int  CreatedBy { get; set; } 
       public DateTime  CreatedTime { get; set; } 
       public int  UpdatedBy { get; set; } 
       public DateTime  UpdatedTime { get; set; } 
   }

}
