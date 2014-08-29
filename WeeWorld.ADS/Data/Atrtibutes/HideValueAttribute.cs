using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeeWorld.ADS.Data.Atrtibutes
{
    
    /// <summary>If added to a property, its values will not be exposed through the api
    /// <seealso cref="IModelFormatter"/></summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HideValueAttribute : Attribute
    {

    }
}
