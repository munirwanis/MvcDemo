//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dados
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    
        public virtual BlogPost Post { get; set; }
        public virtual User User { get; set; }
    }
}
