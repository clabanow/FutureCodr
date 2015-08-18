using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface IBootcampSessionRepository
    {
        List<BootcampSession> GetAllBootcampSessions();
        List<BootcampSession> GetBootcampSessionsByBootcampId(int bootcampId);
        BootcampSession GetBootcampSessionById(int sessionId);
        BootcampSession AddBootcampSession(BootcampSession session);
        void EditBootcampSession(BootcampSession session);
        void DeleteBootcampSession(int sessionId);
    }
}
