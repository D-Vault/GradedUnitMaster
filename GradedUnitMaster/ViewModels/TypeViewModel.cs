
using System;
using System.Linq.Expressions;
using What2Do.Data;

namespace GradedUnitMaster.Models
{
    /// <summary>
    /// Displays the details of event types
    /// </summary>
    public class TypeViewModel
    {

        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }

        /// <summary>
        /// Assigns values to the TypeViewModel
        /// </summary>
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