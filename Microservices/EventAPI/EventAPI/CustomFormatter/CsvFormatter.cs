﻿using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.CustomFormatter
{
    public class CsvFormatter:TextOutputFormatter
    {
        public CsvFormatter() {
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);
            this.SupportedMediaTypes.Add("text/csv");
            this.SupportedMediaTypes.Add("application/csv");
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(EventInfo).IsAssignableFrom(type) ||
                typeof(IEnumerable<EventInfo>).IsAssignableFrom(type))
            {
                return true;
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var buffer = new StringBuilder();
            var response = context.HttpContext.Response;
            if (context.Object is EventInfo)
            {
                var eventinfo = context.Object as EventInfo;
                buffer.AppendLine("Id,Title,Speaker,Organizer,StartDate,EndDate,Location,RegistrationUrl");
                buffer.AppendLine($"{eventinfo.Id},{eventinfo.Title},{eventinfo.Speaker},{eventinfo.Organizer},{eventinfo.StartDate},{eventinfo.EndDate},{eventinfo.Location},{eventinfo.RegistrationUrl}");
            }
            else if(context.Object is IEnumerable<EventInfo>)
            {
                var events = context.Object as IEnumerable<EventInfo>;
                buffer.AppendLine("Id,Title,Speaker,Organizer,StartDate,EndDate,Location,RegistrationUrl");
                foreach (EventInfo eventinfo in events)
                {
                    buffer.AppendLine($"{eventinfo.Id},{eventinfo.Title},{eventinfo.Speaker},{eventinfo.Organizer},{eventinfo.StartDate},{eventinfo.EndDate},{eventinfo.Location},{eventinfo.RegistrationUrl}");
                }
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
