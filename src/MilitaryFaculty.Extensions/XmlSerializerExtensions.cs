using System;
using System.IO;
using System.Xml.Serialization;

namespace MilitaryFaculty.Extensions
{
    public static class XmlSerializerExtensions
    {
        public static T Deserialize<T>(this XmlSerializer @this,
                                       Stream stream)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            return (T) @this.Deserialize(stream);
        }
    }
}