using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public abstract class BaseEntity
{
    // Using GUID would be a far better practice than just the primative type of int, however
    // for this exercise I am using int for simplicity and development purposes.
    public virtual int Id { get; protected set; }
}
