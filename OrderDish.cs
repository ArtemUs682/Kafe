//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kafe
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDish
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }
    
        public virtual Dishes Dishes { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
