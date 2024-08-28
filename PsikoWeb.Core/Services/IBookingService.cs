using Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBookingService
    {
        public void Initialise();
        public Event GetEvent(int id);

        public IList<Event> GetEvents();
        public IList<Event> GetUserEventsForRoom(int roomId);

        public Event AddEvent(Event e);
        public bool DeleteEvent(int id);
        public Event UpdateEvent(Event updated);

        public IEnumerable<Event> GetEventsQuery(Func<Event, bool> q);
        public bool IsValidEvent(Event e);

        public IList<Room> GetRooms();
        public Room GetRoom(int id);
        public Room AddRoom(Room r);
    }
}
