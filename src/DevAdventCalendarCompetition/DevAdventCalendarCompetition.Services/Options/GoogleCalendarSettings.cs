using System.ComponentModel.DataAnnotations;

namespace DevAdventCalendarCompetition.Services.Options
{
    public class GoogleCalendarSettings
    {
        [Required]
        public string Summary { get; set; }

        [Required]
        public CalendarEvent Events { get; set; }

        [Required]
        public string CalendarsEndpoint { get; set; }

        [Required]
        public string EventsEndpoint { get; set; }
    }
}
