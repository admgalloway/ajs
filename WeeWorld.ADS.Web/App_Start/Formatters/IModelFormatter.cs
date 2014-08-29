
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WeeWorld.ADS.Data.Atrtibutes;
using WeeWorld.ADS.Data.Models;
using Newtonsoft.Json;

namespace WeeWorld.ADS.Web.Formatters
{
    public class IModelFormatter : BufferedMediaTypeFormatter
    {
        private bool withBuilds { get; set; }

        public IModelFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public override bool CanWriteType(Type type)
        {
            // only handle IModels (or collections of IModels)
            return (typeof(IModel).IsAssignableFrom(type) | typeof(IEnumerable<IModel>).IsAssignableFrom(type));
        }

        public override bool CanReadType(Type type)
        {
            // let default serializer handle input
            return false;
        }

        public override void WriteToStream(Type type, object value, Stream stream, HttpContent content)
        {
            dynamic output;
            if (value is IModel)
            {
                output = ParseObject(value as IModel);
            }
            else
            {
                output = ParseObjects(value as IEnumerable<IModel>);
            }

            // write output (either an IModel, or a List of IModels)
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(output));
            }
            stream.Close();
        }

        /// <summary>Take an IModel and replace any properties it has that are other IModels with Id values</summary>
        private dynamic ParseObject(IModel obj)
        {
            // if object is null, return null (covers casting issues)
            if (obj == null) return null;

            // create output as dictionary (will parse natively as a dynamic object)
            var output = new Dictionary<string, object>();

            // reflection isnt ideal here,prob okay for small data packets, 
            // but if it grows then it might start getting sluggish

            // loop over the properties on the object
            foreach (var property in obj.GetType().GetProperties())
            {
                var attributes = property.GetCustomAttributes(true);
                foreach (var attr in attributes)
                {
                    var hideValue = attr as HideValueAttribute;
                    if (hideValue != null)
                    {
                        // property value is hidden, so null its value move to next property
                        output.Add(property.Name, null);
                        goto NextProperty;
                    }
                }

                var value = property.GetValue(obj);

                if (value is IModel)
                {
                    // property is a single IModel, so grab its Id and output it in place of the object
                    output.Add(property.Name, (value as IModel).Id);
                }
                else if (value is IEnumerable<IModel>)
                {
                    // property is collection of IModels, grab their ids and return as list of ints
                    output.Add(property.Name, (value as IEnumerable<IModel>).Select(v => v.Id).ToList());
                }
                else
                {
                    // property isn't an IModel, so include it as it is
                    output.Add(property.Name, value);
                }

                NextProperty: continue;
            }

            return output;
        }

        /// <summary>Take a list of IModels and relace any nested IModels</summary>
        private IList<dynamic> ParseObjects(IEnumerable<IModel> objs)
        {
            // if object is null, return null (covers casting issues)
            if (objs == null) return null;

            return objs.Select(o => ParseObject(o)).ToList();
        }
    }
}