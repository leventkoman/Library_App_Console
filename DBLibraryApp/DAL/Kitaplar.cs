//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBLibraryApp.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kitaplar
    {
        public int Id { get; set; }
        public string KitapAdi { get; set; }
        public int YazarId { get; set; }
        public string ISBN { get; set; }
        public int RafId { get; set; }
    
        public virtual Raflar Raflar { get; set; }
        public virtual Yazarlar Yazarlar { get; set; }
    }
}