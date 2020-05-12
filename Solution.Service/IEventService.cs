using Service.Pattern;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public interface IEventService : IService<Event>
    {
        IEnumerable<Event> SearchEventByName(string searchString);
        int Sumpercategory (Category category);
        int SumEvent();
        int SumEducation();
        int SumEntr();

        int SumOther();
        Boolean test(int id, Event ev);
        int NbrParticipant(int id);
        IEnumerable<User> ListParticipantbyEvent(int id);





    }
}
