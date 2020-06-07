using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class EventService : Service<Event>, IEventService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        IParticipationService ParticipationService;
        IUserService UserService;

        public EventService() : base(utk)
        {
            ParticipationService = new ParticipationService();
            UserService = new UserService();
        }

        public void Add(KinderGarten entity)
        {
            throw new NotImplementedException();
        }

        public void commit()
        {
            utk.Commit();
        }

        public void Delete(KinderGarten entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<KinderGarten, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            utk.Dispose();
        }


        public IEnumerable<Event> SearchEventByName(string searchString)
        {
            IEnumerable<Event> EventDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                EventDomain = GetMany(x => x.Name.Contains(searchString));
            }
            return EventDomain;
        }


        public int Sumpercategory(Category category)
        {
            int x = 0;
            IEnumerable<Event> EventDomain = GetMany();
            foreach (var item in EventDomain)
            {
                if (EventDomain.Equals(category))
                    x = +1;
            }
            return x;

        }
        public int SumEvent()
        {

            return GetMany().Count();

        }
        public int SumEducation()
        {
            int sum = 0;
            IEnumerable<Event> EventDomain = GetMany();

            return sum = EventDomain.Where(x => x.Category.ToString() == "Educative").Count();

        }
        public int SumOther()
        {
            int sum = 0;
            IEnumerable<Event> EventDomain = GetMany();

            return sum = EventDomain.Where(x => x.Category.ToString() == "Other").Count();

        }
        public int SumEntr()
        {

            int sum = 0;
            IEnumerable<Event> EventDomain = GetMany();

            return sum = EventDomain.Where(x => x.Category.ToString() == "Entertainment").Count();

        }

        public Boolean test(int id, Event ev)
        {
            foreach (var p in ParticipationService.GetMany())
            {
                if (p.EventId == ev.EventId && p.ParentId == id)
                    return true;
            }

            return false;
        }
        public int NbrParticipant(int idEvent)
        {
            int x = 0;
            foreach (var p in ParticipationService.GetMany())
            {
                if (p.EventId == idEvent)
                    x += 1;
            }
            return x;
        }
        public IEnumerable<User> ListParticipantbyEvent(int eventId)
        {
            IEnumerable<Participation> ParticipationDomain = ParticipationService.GetMany();
            return ParticipationDomain.Where(p => p.EventId == eventId).Select(p => UserService.GetById(p.ParentId));
        }
        public Event FindEventByQrCode(string qrcode)
        {
            Event @event;
            @event = GetMany().Where(e => e.qrCode == qrcode).FirstOrDefault();
            return @event;
        }

    }
}
