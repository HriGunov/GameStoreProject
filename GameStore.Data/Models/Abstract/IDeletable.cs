using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Data.Models.Abstract
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}