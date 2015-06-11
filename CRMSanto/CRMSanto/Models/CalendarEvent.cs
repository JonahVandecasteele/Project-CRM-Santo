using Microsoft.Office365.OutlookServices;
using CRMSanto.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CRMSanto.Models
{
    public class CalendarEvent
    {
        public string ID;

        public string Subject { get; set; }

        public string Location { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy HH:mm tt}")]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy HH:mm tt}")]
        public DateTimeOffset EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public string Attendees { get; set; }

        // These variables indicate whether an event is the first or last event in the result set. 
        public bool IsFirstItem { get; set; }
        public bool IsLastItem { get; set; }

        private CalendarOperations _calenderOperations = new CalendarOperations();

        public CalendarEvent(IEvent serverEvent)
        {
            IsLastItem = false;
            IsFirstItem = false;

            string bodyContent = string.Empty;
            if (serverEvent.Body != null)
                bodyContent = serverEvent.Body.Content;

            ID = serverEvent.Id;
            Subject = serverEvent.Subject;
            Location = serverEvent.Location.DisplayName;
            StartDate = (DateTimeOffset)serverEvent.Start.Value.ToLocalTime();
            EndDate = (DateTimeOffset)serverEvent.End.Value.ToLocalTime();


            // Remove HTML tags if the body is returned as HTML.
            string bodyType = serverEvent.Body.ContentType.ToString();
            if (bodyType == "HTML")
            {
                bodyContent = Regex.Replace(bodyContent, "<[^>]*>", "");
                bodyContent = Regex.Replace(bodyContent, "\n", "");
                bodyContent = Regex.Replace(bodyContent, "\r", "");
            }
            Body = bodyContent;
            Attendees = _calenderOperations.BuildAttendeeList(serverEvent.Attendees);
        }
    }
}
