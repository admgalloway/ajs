using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WeeWorld.ADS.Data.Models
{
    public class PList : Dictionary<string, dynamic>
    {
        /// <summary>Accepts a list of xml elements, and turns them into an dictionary of keyvaluepairs</summary>
        public PList(string xmlPath)
        {
            var elements = XDocument.Load(xmlPath).Element("plist").Element("dict").Elements().ToList();

            for (int i = 0; i < elements.Count - 1; i = i + 2)
            {
                if (elements[i].Name == "key")
                {
                    var name = elements[i].Value.ToString();
                    var value = elements[i + 1].Value;
                    Add(name, value);
                }
            }
        }
    }
}
