
using System;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    public class TypeViewModel
    {

        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }

        public static Expression<Func<What2Do.Data.Type, TypeViewModel>> ViewModel
        {
            get
            {
                return t => new TypeViewModel()
                {
                    Id = t.TypeID,
                    TypeName = t.TypeName,
                    TypeDescription = t.TypeDescription
                };
            }
        }
    }
}