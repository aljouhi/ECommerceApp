using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceApp.Helper
{
    public class KotakPesan
    {
        public static string GetPesan(string judul,string jenis, string keterangan)
        {
            return string.Format(@"<div class='alert alert-{0}'>
            <a href='#' class='close' data-dismiss='alert' aria-label='close'> &times;</a>
            <strong>{1}</strong>{2}</div>",judul, jenis, keterangan);
        }
       
    }
}