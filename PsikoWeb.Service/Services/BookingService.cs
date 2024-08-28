using Core.Models.Concrete;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        public BookingService(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public Event AddEvent(Event e)
        {
            if (e == null)
            {
                return null;
            }

            var s = new Event
            {
                Title = e.Title,
                Description = e.Description,
                Start = e.Start,
                End = e.End,
                UserId = e.UserId,
                RoomId = e.RoomId
            };

            // verify event business logic here - no overlapping events etc.
            if (!IsValidEvent(s))
            {
                return null;
            }
            _appDbContext.Events.Add(s);
            _appDbContext.SaveChanges(); // write to database        
            return s; // return newly added Event
        }

        public Room AddRoom(Room room)
        {
            var exists = _appDbContext.Rooms.FirstOrDefault(r => r.Name == room.Name);
            if (exists != null)
            {
                return null;
            }

            _appDbContext.Rooms.Add(room);
            _appDbContext.SaveChanges();
            return room;
        }

        public bool DeleteEvent(int id)
        {
            var s = GetEvent(id);
            if (s == null)
            {
                return false;
            }
            _appDbContext.Events.Remove(s);
            _appDbContext.SaveChanges(); // write to database
            return true;
        }

        public Event GetEvent(int id)
        {
            return _appDbContext.Events.FirstOrDefault(s => s.Id == id);
        }

        public IList<Event> GetEvents()
        {
            return _appDbContext.Events.ToList();
        }

        public IEnumerable<Event> GetEventsQuery(Func<Event, bool> q)
        {
            return _appDbContext.Events.Where(q);
        }

        public Room GetRoom(int id)
        {
            return _appDbContext.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public IList<Room> GetRooms()
        {
            return _appDbContext.Rooms.ToList();
        }

        public IList<Event> GetUserEventsForRoom(int roomId)
        {
            return _appDbContext.Events.Where(e => e.RoomId == roomId).ToList();
        }

        public void Initialise()
        {
            throw new NotImplementedException();
        }

        public bool IsValidEvent(Event n)
        {
            if (n.Start > n.End)
            {
                return false;
            }
            // count number of overlapping events 
            var count = _appDbContext.Events.Count(
                e => e.Id != n.Id &&                                        // exclude event being checked
                     (n.Start < e.Start && n.End > e.Start ||                    // event overlaps start of existing event
                     n.Start >= e.Start & n.Start < e.End && n.End > e.Start)    // event overlaps end of existing event
            );
            return count == 0;
        }

        public Event UpdateEvent(Event updated)
        {
            // verify the Event exists
            var e = GetEvent(updated.Id);
            if (e == null)
            {
                return null;
            }

            // verify event business logic here - no overlapping events etc.
            if (!IsValidEvent(updated))
            {
                return null;
            }

            // update the details of the Event retrieved and save
            e.Title = updated.Title;
            e.Description = updated.Description;
            e.Start = updated.Start;
            e.End = updated.End;
            e.RoomId = updated.RoomId;
            e.UserId = updated.UserId;

            _appDbContext.SaveChanges(); // write to database
            return e;
        }
    }
}
