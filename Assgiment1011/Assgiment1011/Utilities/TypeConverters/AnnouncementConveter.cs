using Assgiment1011.Models.DTOs;
using System.ComponentModel;
using System.Globalization;

namespace Assgiment1011.Utilities.TypeConverters
{
    public class AnnouncementConveter: TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if(value is string s)
            {
                AnnouncementDTO dto;
                if(AnnouncementDTO.TryParse(s, out dto))
                {
                    return dto;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
