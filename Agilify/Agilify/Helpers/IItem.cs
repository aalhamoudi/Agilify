using System;
using System.Collections.Generic;
using System.Text;
using Agilify.Models;

namespace Agilify.Helpers
{
    public interface IItem
    {
        string Id { get; set; }
        string Name { get; set; }
        Member Owner { get; set; }
    }
}
